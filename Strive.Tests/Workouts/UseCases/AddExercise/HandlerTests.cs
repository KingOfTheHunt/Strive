using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Workouts.UseCases.AddExercise;
using Strive.Application.Workouts.UseCases.AddExercise.Contracts;
using Strive.Core.Entities;

namespace Strive.Tests.Workouts.UseCases.AddExercise;

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
    public async Task ShouldReturnTrueWhenAnExerciseIsAddedToAWorkout()
    {
        var request = new Request(1, 1, 1,3, 10, null, null);
        _repositoryMock.Setup(r => r.GetWorkoutByIdAsync(request.WorkoutId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync(new Workout("Super treino", 1));
        _repositoryMock.Setup(r => r.AnyExerciseAsync(request.ExerciseId,
            It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenAnExerciseIsAddedTwiceToAWorkout()
    {
        var workout = new Workout("Super treino", 1);
        var request = new Request(1, 1, 1, 3, 10, null, null);
        _repositoryMock.Setup(r => r.GetWorkoutByIdAsync(request.WorkoutId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync(workout);
        _repositoryMock.Setup(r => r.AnyExerciseAsync(request.ExerciseId,
            It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var firstResult = await _handler.HandleAsync(request, CancellationToken.None);
        var secondResult = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.True(firstResult.Success);
        Assert.False(secondResult.Success);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenAnNonExistentExerciseIsAddToAWorkout()
    {
        var request = new Request(1, 1, 1,3, 10, null, null);
        _repositoryMock.Setup(r => r.GetWorkoutByIdAsync(request.WorkoutId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync(new Workout("Super treino", 1));
        _repositoryMock.Setup(r => r.AnyExerciseAsync(request.ExerciseId,
            It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }
}