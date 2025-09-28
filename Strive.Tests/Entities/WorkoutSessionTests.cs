using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Tests.Entities;

public class WorkoutSessionTests
{
    private readonly WorkoutCommentary _commentary;
    private readonly ScheduledAt _scheduledAt;
    

    public WorkoutSessionTests()
    {
        _commentary = new WorkoutCommentary("Alguma coisa...");
        _scheduledAt = new ScheduledAt(DateTime.UtcNow.AddDays(2));
    }
    
    [Fact]
    public void ShouldReturnTrueWhenWorkoutSessionHasCommentaryAndScheduledAt()
    {
        var workoutSession = new WorkoutSession(1, _commentary, _scheduledAt);
        
        Assert.True(workoutSession.IsValid);
    }

    [Fact]
    public void ShouldReturnTrueWheWorkoutSessionHasCommentary()
    {
        var workoutSession = new WorkoutSession(1, _commentary);
        
        Assert.True(workoutSession.IsValid);
    }

    [Fact]
    public void ShouldReturnTrueWhenWorkoutSessionHasScheduledAt()
    {
        var workoutSession = new WorkoutSession(1, _scheduledAt);
        
        Assert.True(workoutSession.IsValid);
    }

    [Fact]
    public void ShouldAddACommentaryWhenWorkoutCommentaryIsValid()
    {
        var workoutSession = new WorkoutSession(1, _scheduledAt);
        var commentary = "A new comentary...";
        workoutSession.AddWorkoutCommentary(commentary);
        
        Assert.Equal(commentary, workoutSession.Commentary!.Commentary);
    }

    [Fact]
    public void ShouldNotAddACommentaryWhenWorkoutCommentaryIsNotValid()
    {
        var workoutSession = new WorkoutSession(1, _commentary, _scheduledAt);
        var currentCommentary = workoutSession.Commentary!.Commentary;
        workoutSession.AddWorkoutCommentary("");
        
        Assert.Equal(currentCommentary, workoutSession.Commentary.Commentary);
    }

    [Fact]
    public void ShouldScheduleAWorkoutWhenScheduledAtIsValid()
    {
        var workoutSession = new WorkoutSession(1);
        var scheduleDate = DateTime.UtcNow.AddDays(2);
        workoutSession.ScheduleWorkout(scheduleDate);
        
        Assert.Equal(scheduleDate, workoutSession.ScheduledAt!.Date);
    }

    [Fact]
    public void ShouldNotScheduleAWorkoutWhenScheduledAtIsInvalid()
    {
        var workoutSession = new WorkoutSession(1, _scheduledAt);
        var currentScheduleDate = workoutSession.ScheduledAt!.Date;
        workoutSession.ScheduleWorkout(DateTime.UtcNow.AddDays(-1));
        
        Assert.Equal(currentScheduleDate, workoutSession.ScheduledAt.Date);
    }
}