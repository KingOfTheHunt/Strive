using Strive.Core.ValueObjects;

namespace Strive.Tests.ValueObjects;

public class ExerciseWeightTests
{
    [Fact]
    public void ShouldReturnTrueWhenExerciseWeightIsGreaterThan0Kg()
    {
        var weight = 10.0f;

        var exerciseWeight = new ExerciseWeight(weight);
        
        Assert.True(exerciseWeight.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenExerciseWeightIsLowerThan0Kg()
    {
        var weight = -1.0f;

        var exerciseWeight = new ExerciseWeight(weight);
        
        Assert.False(exerciseWeight.IsValid);
    }
}