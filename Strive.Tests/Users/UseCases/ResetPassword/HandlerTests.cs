using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Users.UseCases.ResetPassword;
using Strive.Application.Users.UseCases.ResetPassword.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Tests.Users.UseCases.ResetPassword;

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
    public async Task ShouldReturnTrueWhenResetCodeIsCorrect()
    {
        var name = new Name("John", "Doe");
        var email = new Email("jdoe@email.com");
        var password = new Password("JohnDoe123@");
        var request = new Request(email.Address, "Abc123!@", "Abc123!@",
            password.ResetCode);
        _repositoryMock.Setup(r => r.GetUserByEmailAsync(request.Email,
            It.IsAny<CancellationToken>())).ReturnsAsync(new User(name, email, password));

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenResetCodeIsIncorrect()
    {
        var name = new Name("John", "Doe");
        var email = new Email("jdoe@email.com");
        var password = new Password("JohnDoe123@");
        var request = new Request(email.Address, "Abc123!@", "Abc123!@",
            "000ooo");
        _repositoryMock.Setup(r => r.GetUserByEmailAsync(request.Email,
            It.IsAny<CancellationToken>())).ReturnsAsync(new User(name, email, password));

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenThePasswordsAreNotTheSame()
    {
        var name = new Name("John", "Doe");
        var email = new Email("jdoe@email.com");
        var password = new Password("JohnDoe123@");
        var request = new Request(email.Address, "Abc123!@", "Abc123@!",
            password.ResetCode);
        _repositoryMock.Setup(r => r.GetUserByEmailAsync(request.Email,
            It.IsAny<CancellationToken>())).ReturnsAsync(new User(name, email, password));

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }
}