using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Workouts.UseCases.Create;
using Strive.Application.Workouts.UseCases.Create.Contracts;

namespace Strive.Tests.Workouts.UseCases.Create;

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
    public async Task ShouldReturnTrueWhenANewWorkoutIsCreated()
    {
        var request = new Request("Super treino de costas", 1);
        _repositoryMock.Setup(r => r.AnyWorkoutAsync(request.Name,
            It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.True(result.Success);
        Assert.Equal(201, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenAWorkoutHasTheSameNameAsOtherWorkout()
    {
        var request = new Request("Super treino de costas", 1);
        _repositoryMock.Setup(r => r.AnyWorkoutAsync(request.Name,
            It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenAWorkoutHasAEmptyName()
    {
        var request = new Request("", 1);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal("Dados inválidos.", result.Message);
        Assert.Equal(400, result.StatusCode);
    }
}