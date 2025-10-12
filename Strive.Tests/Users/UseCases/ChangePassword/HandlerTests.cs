using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Users.UseCases.ChangePassword;
using Strive.Application.Users.UseCases.ChangePassword.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Tests.Users.UseCases.ChangePassword;

public class HandlerTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly Mock<ILogger<Handler>> _loggerMock;
    private readonly Handler _handler;

    public HandlerTests()
    {
        _repositoryMock = new();
        _loggerMock = new();
        _handler = new(_repositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task ShouldChangePasswordWhenBothPasswordsAreEqual()
    {
        var name = new Name("John", "Doe");
        var email = new Email("jdoe@email.com");
        var password = new Password("OldPassword12@");
        var request = new Request(1, "newPassword123@", "newPassword123@");
        _repositoryMock.Setup(r => r.GetUserByIdAsync(request.Id,
            It.IsAny<CancellationToken>())).ReturnsAsync(new User(name, email, password));

        var result = await _handler.HandleAsync(request);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenPasswordsAreDifferent()
    {
        var name = new Name("John", "Doe");
        var email = new Email("jdoe@email.com");
        var password = new Password("OldPassword12@");
        var request = new Request(1, "newPassword123@", "newPassword@123");
        _repositoryMock.Setup(r => r.GetUserByIdAsync(request.Id,
            It.IsAny<CancellationToken>())).ReturnsAsync(new User(name, email, password));

        var result = await _handler.HandleAsync(request);

        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenNewPasswordIsInvalid()
    {
        var name = new Name("John", "Doe");
        var email = new Email("jdoe@email.com");
        var password = new Password("OldPassword12@");
        var request = new Request(1, "newPassword", "newPassword");
        _repositoryMock.Setup(r => r.GetUserByIdAsync(request.Id,
            It.IsAny<CancellationToken>())).ReturnsAsync(new User(name, email, password));

        var result = await _handler.HandleAsync(request);
        
        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }
}