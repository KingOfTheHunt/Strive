using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Users.UseCases.SendPasswordResetCode;
using Strive.Application.Users.UseCases.SendPasswordResetCode.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Tests.Users.UseCases.SendPasswordRestCode;

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
    public async Task ShouldReturnTrueWhenEmailExistInTheDatabase()
    {
        var name = new Name("John", "Doe");
        var email = new Email("jdoe@email.com");
        var password = new Password("Password123@");
        var request = new Request(email.Address);
        _repositoryMock.Setup(r => r.GetUserByEmailAsync(request.Email,
            It.IsAny<CancellationToken>())).ReturnsAsync(new User(name, email, password));

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        _emailServiceMock.Verify(e => e.SendResetPasswordCodeEmail(It.IsAny<User>(), 
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenEmailDoesNotExistInTheDatabase()
    {
        var request = new Request("some@email.com");
        _repositoryMock.Setup(r => r.GetUserByEmailAsync(request.Email,
            It.IsAny<CancellationToken>())).ReturnsAsync((User?) null);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }
}