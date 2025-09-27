using Flunt.Notifications;
using Strive.Core.ValueObjects;

namespace Strive.Tests.ValueObjects;

public class WorkoutSetsTests
{
    [Fact]
    public void ShouldReturnTrueWhenWorkoutSetsHas3Sets()
    {
        byte sets = 3;
        var workoutSets = new WorkoutSets(sets);
        
        Assert.True(workoutSets.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenWorkoutSetsHas10Sets()
    {
        byte sets = 10;
        var workoutSets = new WorkoutSets(sets);
        
        Assert.False(workoutSets.IsValid);
        Assert.Equal("O número de séries precisa ser menor ou igual a 5.", 
            GetNotification(workoutSets.Notifications));
    }

    private string GetNotification(IReadOnlyCollection<Notification> notifications) => 
        notifications.Select(x => x.Message).FirstOrDefault("");
}