using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Workouts.UseCases.RemoveExercise;
using Strive.Application.Workouts.UseCases.RemoveExercise.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Tests.Workouts.UseCases.RemoveExercise;

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
    public async Task ShouldReturnTrueWhenAnExerciseIsRemovedFromOfAWorkout()
    {
        var sets = new WorkoutSets(3);
        var repetitions = new WorkoutRepetitions(10);
        var workoutExercise = new WorkoutExercise(1, 1, sets, repetitions, null,
            null);
        var workout = new Workout("Super treino teste", 1);
        workout.AddExercise(workoutExercise);
        var request = new Request(1, 1, 1);
        _repositoryMock.Setup(r => r.GetWorkoutByIdAsync(request.ExerciseId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync(workout);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenAnNonExistExerciseIsRemovedFromAWorkout()
    {
        var request = new Request(1, 1, 1);
        _repositoryMock.Setup(r => r.GetWorkoutByIdAsync(request.ExerciseId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync(new Workout("Super treino", 1));

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }
}