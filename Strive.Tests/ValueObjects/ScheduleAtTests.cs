using Strive.Core.ValueObjects;

namespace Strive.Tests.ValueObjects;

public class ScheduleAtTests
{
    [Fact]
    public void ShouldReturnTrueWhenScheduledAtIsValid()
    {
        var date = DateTime.Now.AddDays(2);

        var scheduledAt = new ScheduledAt(date);
        
        Assert.True(scheduledAt.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenWorkoutIsScheduledInThePast()
    {
        var date = DateTime.Now.AddDays(-2);

        var scheduledAt = new ScheduledAt(date);
        
        Assert.False(scheduledAt.IsValid);
    }
}