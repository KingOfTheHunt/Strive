using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Users.UseCases.ResendVerification;
using Strive.Application.Users.UseCases.ResendVerification.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Tests.Users.UseCases.ResendVerification;

public class HandlerTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly Mock<ILogger<Handler>> _loggerMock;
    private readonly Handler _handler;

    public HandlerTests()
    {
        _repositoryMock = new();
        _emailServiceMock = new();
        _loggerMock = new();
        _handler = new(_repositoryMock.Object, _emailServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task ShouldReturnTrueWhenEmailExistsInTheDatabase()
    {
        var name = new Name("John", "Doe");
        var email = new Email("jdoe@email.com");
        var password = new Password("Abc123!@");
        var request = new Request("jdoe@email.com");
        _repositoryMock.Setup(r => r.GetUserByEmailAsync(request.Email,
            It.IsAny<CancellationToken>())).ReturnsAsync(new User(name, email, password));

        var result = await _handler.HandleAsync(request);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        _emailServiceMock.Verify(e => e.SendEmailAsync(It.IsAny<User>(), 
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenEmailDoesNotExistsInTheDatabase()
    {
        var request = new Request("doesnotexits@email.com");
        _repositoryMock.Setup(r => r.GetUserByEmailAsync(request.Email,
            It.IsAny<CancellationToken>())).ReturnsAsync((User?) null);

        var result = await _handler.HandleAsync(request);
        
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }
}