using Flunt.Notifications;
using Strive.Core.ValueObjects;

namespace Strive.Tests.ValueObjects;

public class WorkoutRepetitionsTests
{
    [Fact]
    public void ShouldReturnTrueWhenWorkoutRepetitionsHas5Repetitions()
    {
        byte repetitions = 5;
        var workoutRepetitions = new WorkoutRepetitions(repetitions);
        
        Assert.True(workoutRepetitions.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenWorkoutRepetitionsHas35Repetitions()
    {
        byte repetitions = 35;
        var workoutRepetitions = new WorkoutRepetitions(repetitions);
        
        Assert.False(workoutRepetitions.IsValid);
        Assert.Equal("O número de repetições precisa ser menor ou igual a 20.",
            GetNotification(workoutRepetitions.Notifications));
    }

    private string GetNotification(IReadOnlyCollection<Notification> notifications) =>
        notifications.Select(x => x.Message).FirstOrDefault("");
}