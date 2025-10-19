using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Workouts.UseCases.UpdateExercise;
using Strive.Application.Workouts.UseCases.UpdateExercise.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Tests.Workouts.UseCases.UpdateExercise;

public class HandlerTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly Mock<ILogger<Handler>> _logger;
    private readonly Handler _handler;

    public HandlerTests()
    {
        _repositoryMock = new();
        _logger = new();
        _handler = new(_repositoryMock.Object, _logger.Object);
    }

    [Fact]
    public async Task ShouldReturnTrueWhenAnExerciseIsUpdated()
    {
        var workout = new Workout("Super treino teste", 1);
        var workoutExercise = new WorkoutExercise(1, 1, new WorkoutSets(3), 
            new WorkoutRepetitions(10), new ExerciseWeight(5.0f), null);
        workout.AddExercise(workoutExercise);
        var request = new Request(1, 1, 1, 5, 15, null, null);
        _repositoryMock.Setup(r => r.GetWorkoutByIdAsync(request.WorkoutId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync(workout);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenExerciseDoesNotExistInTheWorkout()
    {
        var workout = new Workout("Super treino teste", 1);
        var request = new Request(1, 1, 1, 3, null, null, 90);
        _repositoryMock.Setup(r => r.GetWorkoutByIdAsync(request.WorkoutId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync(workout);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenWorkoutDoesNotExist()
    {
        var request = new Request(1, 1, 1, 3, 10, 15, null);
        _repositoryMock.Setup(r => r.GetWorkoutByIdAsync(request.WorkoutId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync((Workout)null);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal("Não há nenhum treino com este Id.", result.Message);
        Assert.Equal(404, result.StatusCode);
    }
}