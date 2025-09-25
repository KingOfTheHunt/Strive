using Strive.Core.ValueObjects;

namespace Strive.Tests.ValueObjects;

public class WorkoutCommentaryTests
{
    [Fact]
    public void ShouldReturnTrueWhenCommentaryIsValid()
    {
        var commentary = "O treino foi extremamente produtivo hoje.";
        
        var workoutCommentary = new WorkoutCommentary(commentary);
        
        Assert.True(workoutCommentary.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenCommentaryHasLessThan3Chars()
    {
        var commentaryWithToChars = "aa";

        var workoutCommentary = new WorkoutCommentary(commentaryWithToChars);
        
        Assert.False(workoutCommentary.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenCommentaryHasMoreThan200Chars()
    {
        var commentaryWithMoreThan200Chars = new string('a', 201);

        var workoutCommentary = new WorkoutCommentary(commentaryWithMoreThan200Chars);
        
        Assert.False(workoutCommentary.IsValid);
    }
}