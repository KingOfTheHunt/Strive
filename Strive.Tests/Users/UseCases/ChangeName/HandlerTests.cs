using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Users.UseCases.ChangeName;
using Strive.Application.Users.UseCases.ChangeName.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Tests.Users.UseCases.ChangeName;

public class HandlerTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly Mock<ILogger<Handler>> _loogerMock;
    private readonly Handler _handler;

    public HandlerTests()
    {
        _repositoryMock = new();
        _loogerMock = new();
        _handler = new(_repositoryMock.Object, _loogerMock.Object);
    }

    [Fact]
    public async Task ShouldReturnTrueWhenNewNameIsValid()
    {
        var oldName = new Name("Johnathan", "Doe");
        var email = new Email("jdoe@email.com");
        var password = new Password("Abc123!@");
        var request = new Request(1, "John", "Doe");
        _repositoryMock.Setup(r => r.GetUserByIdAsync(request.Id,
            It.IsAny<CancellationToken>())).ReturnsAsync(new User(oldName, email, password));

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenUserDoesNotExistInTheDatabase()
    {
        var request = new Request(1, "John", "Doe");
        _repositoryMock.Setup(r => r.GetUserByIdAsync(request.Id,
            It.IsAny<CancellationToken>())).ReturnsAsync((User?)null);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }
}