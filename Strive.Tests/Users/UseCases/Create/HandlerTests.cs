using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Users.UseCases.Create;
using Strive.Application.Users.UseCases.Create.Contracts;
using Strive.Core.Entities;

namespace Strive.Tests.Users.UseCases.Create;

public class HandlerTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly Mock<ILogger<Handler>> _loggerMock;
    private readonly Handler _handler;

    public HandlerTests()
    {
        _repositoryMock = new Mock<IRepository>();
        _emailServiceMock = new Mock<IEmailService>();
        _loggerMock = new Mock<ILogger<Handler>>();
        _handler = new Handler(_repositoryMock.Object, _emailServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task ShouldReturnTrueWhenRequestIsValid()
    {
        var request = new Request("John", "Doe", "jdoe@email.com", "Abc123!@");

        _repositoryMock.Setup(r => r.AnyEmailAsync(request.Email,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var response = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.True(response.Success);
        Assert.Equal(201, response.StatusCode);
        _repositoryMock.Verify(r => r.SaveAsync(It.IsAny<User>(), 
            It.IsAny<CancellationToken>()), Times.Once);
        _emailServiceMock.Verify(m => m.SendWelcomeEmail(It.IsAny<User>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenEmailAlreadyExistsInDatabase()
    {
        var request = new Request("John", "Doe", "jdoe@email.com", "Abc123!@");

        _repositoryMock.Setup(r => r.AnyEmailAsync(request.Email,
            It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal(409, result.StatusCode);
        _repositoryMock.Verify(r => r.SaveAsync(It.IsAny<User>(), 
            It.IsAny<CancellationToken>()), Times.Never);
        _emailServiceMock.Verify(e => e.SendWelcomeEmail(It.IsAny<User>(), 
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenRequestIsInvalid()
    {
        var request = new Request("", "", "", "");

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }
}