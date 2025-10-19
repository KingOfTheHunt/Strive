using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Workouts.UseCases.Schedule.Contracts;
using Strive.Application.Workouts.UseCases.Schedule;
using Strive.Core.Entities;

namespace Strive.Tests.Workouts.UseCases.Schedule;

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
    public async Task ShouldReturnTrueWhenWorkoutIsScheduled()
    {
        var workout = new Workout("Super treino de costas.", 1);
        var request = new Request(1, 1, DateTime.UtcNow.AddHours(3));
        _repositoryMock.Setup(r => r.GetWorkoutByIdAsync(request.WorkoutId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync(workout);

        var result = await _handler.HandleAsync(request);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenWorkoutIsScheduledInThePast()
    {
        var workout = new Workout("Super treino de costas", 1);
        var request = new Request(1, 1, DateTime.UtcNow.AddHours(-2));
        _repositoryMock.Setup(r => r.GetWorkoutByIdAsync(request.WorkoutId, request.UserId,
            It.IsAny<CancellationToken>())).ReturnsAsync(workout);

        var result = await _handler.HandleAsync(request);
        
        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }
}