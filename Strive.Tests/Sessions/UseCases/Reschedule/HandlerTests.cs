using Microsoft.Extensions.Logging;
using Moq;
using Strive.Application.Sessions.UseCases.Reschedule.Contracts;
using Strive.Application.Sessions.UseCases.Reschedule;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Tests.Sessions.UseCases.Reschedule;

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
    public async Task ShouldReturnTrueWhenWorkoutIsRescheduled()
    {
        var scheduledAt = new ScheduledAt(DateTime.UtcNow.AddDays(3));
        var workoutSession = new WorkoutSession(1, scheduledAt);
        var request = new Request(1, 1, DateTime.UtcNow.AddDays(2));
        _repositoryMock.Setup(r => r.GetWorkoutSessionByIdAsync(request.WorkoutSessionId,
            request.UserId, It.IsAny<CancellationToken>())).ReturnsAsync(workoutSession);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenWorkoutIsRescheduledInThePast()
    {
        var scheduledAt = new ScheduledAt(DateTime.UtcNow.AddDays(1));
        var workoutSession = new WorkoutSession(1, scheduledAt);
        var request = new Request(1, 1, DateTime.UtcNow.AddDays(-2));
        _repositoryMock.Setup(r => r.GetWorkoutSessionByIdAsync(request.WorkoutSessionId,
            request.UserId, It.IsAny<CancellationToken>())).ReturnsAsync(workoutSession);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenWorkoutSessionDoesNotExist()
    {
        var request = new Request(4, 1, DateTime.UtcNow.AddDays(4));
        _repositoryMock.Setup(r => r.GetWorkoutSessionByIdAsync(request.WorkoutSessionId,
            request.UserId, It.IsAny<CancellationToken>())).ReturnsAsync((WorkoutSession)null);

        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }
}