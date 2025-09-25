using Strive.Core.ValueObjects;

namespace Strive.Tests.ValueObjects;

public class ExerciseTimeTests
{
    [Fact]
    public void ShouldReturnTrueWhenExerciseTimeIsGreaterOrEqualsThan10Seconds()
    {
        var timeInSeconds = 10;

        var exerciseTime = new ExerciseTime(timeInSeconds);
        
        Assert.True(exerciseTime.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenExerciseTimeIsLowerThan10Seconds()
    {
        var timeInSeconds = 5;

        var exerciseTime = new ExerciseTime(timeInSeconds);
        
        Assert.False(exerciseTime.IsValid);
    }
}