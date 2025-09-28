using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Tests.Entities;

public class WorkoutTests
{
    [Fact]
    public void ShouldReturnTrueWhenWorkoutIsValid()
    {
        string workoutName = "Super treino de costas";
        var workout = new Workout(workoutName, 1);
        
        Assert.True(workout.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenWorkoutIsInvalid()
    {
        var workoutName = "ac";
        var workout = new Workout(workoutName, 1);
        
        Assert.False(workout.IsValid);
    }

    [Fact]
    public void ShouldAddAnExerciseWhenWorkoutExerciseIsUnique()
    {
        var workoutName = "Super treino de costas";
        var workout = new Workout(workoutName, 1);
        var workoutExercise = new WorkoutExercise(1, 1, new WorkoutSets(3), new ExerciseTime(90));
        workout.AddExercise(workoutExercise);

        Assert.Single(workout.WorkoutExercises);
    }

    [Fact]
    public void ShouldNotAddAnExerciseWhenWorkoutExerciseIsDuplicated()
    {
        var workoutName = "Super treino de costas";
        var workout = new Workout(workoutName, 1);
        var workoutExercise = new WorkoutExercise(1, 1, new WorkoutSets(3), new ExerciseTime(90));
        
        workout.AddExercise(workoutExercise);
        workout.AddExercise(workoutExercise);
        
        Assert.False(workout.IsValid);
    }
}