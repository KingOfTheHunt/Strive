using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Users.UseCases.Authenticate;
using Strive.Application.Users.UseCases.Authenticate.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Tests.Users.UseCases.Authenticate;

public class HandlerTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly Mock<ILogger<Handler>> _loggerMock;
    private readonly Handler _handler;

    public HandlerTests()
    {
        _repositoryMock = new Mock<IRepository>();
        _loggerMock = new Mock<ILogger<Handler>>();
        _handler = new Handler(_repositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task ShouldReturnTrueWhenEmailAndPasswordAreCorrects()
    {
        var name = new Name("John", "Doe");
        var email = new Email("jdoe@email.com");
        var password = new Password("Abc123!@");
        var request = new Request("jdoe@email.com", "Abc123!@");
        var user = new User(name, email, password);
        var verificationCode = user.Email.Verification.Code;
        user.Email.Verification.Verify(verificationCode);
        _repositoryMock.Setup(r => r.GetUserByEmailAsync("jdoe@email.com",
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var result = await _handler.HandleAsync(request);

        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenPasswordIsIncorrect()
    {
        var name = new Name("John", "Doe");
        var email = new Email("jdoe@email.com");
        var password = new Password("Abc123!@");
        var request = new Request("jdoe@email.com", "senhaInvalida123@");
        _repositoryMock.Setup(r => r.GetUserByEmailAsync("jdoe@emailcom",
            It.IsAny<CancellationToken>())).ReturnsAsync(new User(name, email, password));

        var result = await _handler.HandleAsync(request);
        
        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }
}