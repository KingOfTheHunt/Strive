using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Tests.Entities;

public class WorkoutExerciseTests
{
    private readonly WorkoutSets _validSets;
    private readonly WorkoutRepetitions _validRepetitions, _invalidRepetitions;
    private readonly ExerciseWeight _validWeight, _invalidWeight;
    private readonly ExerciseTime _validDuration, _invalidDuration;

    public WorkoutExerciseTests()
    {
        _validSets = new WorkoutSets(3);
        _validRepetitions = new WorkoutRepetitions(10);
        _invalidRepetitions = new WorkoutRepetitions(255);
        _validWeight = new ExerciseWeight(10.0f);
        _invalidWeight = new ExerciseWeight(200f);
        _validDuration = new ExerciseTime(90);
        _invalidDuration = new ExerciseTime(0);
    }
    
    [Fact]
    public void ShouldReturnTrueWhenWorkoutExerciseIsValid()
    {
        var workoutExercise = new WorkoutExercise(1, 1, _validSets, _validRepetitions, 
            _validWeight);
        
        Assert.True(workoutExercise.IsValid);
    }

    [Fact]
    public void ShouldReturnTrueWhenWorkoutExerciseHasSetsAndRepetitions()
    {
        var workoutExercise = new WorkoutExercise(1, 1, _validSets, _validRepetitions);
        
        Assert.True(workoutExercise.IsValid);
    }

    [Fact]
    public void ShouldReturnTrueWhenWorkoutExerciseHasSetsAndDuration()
    {
        var workoutExercise = new WorkoutExercise(1, 1, _validSets, _validDuration);
        
        Assert.True(workoutExercise.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenWorkoutExerciseHasNotSets()
    {
        var workoutExercise = new WorkoutExercise(1, 1, null, _validRepetitions, _validWeight);
        
        Assert.False(workoutExercise.IsValid);
    }

    [Fact]
    public void ShouldChangeWorkoutSetsWhenSetsIsValid()
    {
        byte newSets = 5;
        var sets = new WorkoutSets(3);
        var repetitions = new WorkoutRepetitions(10);
        var workoutExercise = new WorkoutExercise(1, 1, sets, repetitions);
        
        workoutExercise.UpdateSets(newSets);
        
        Assert.Equal(newSets, workoutExercise.Sets.Sets);
    }
    
    [Fact]
    public void ShouldNotChangeWorkoutSetsWhenSetsIsInvalid()
    {
        var sets = new WorkoutSets(3);
        var repetitions = new WorkoutRepetitions(10);
        var workoutExercise = new WorkoutExercise(1, 1, sets, repetitions);
        var currentSets = workoutExercise.Sets.Sets;
        
        workoutExercise.UpdateSets(255);

        Assert.Equal(currentSets, workoutExercise.Sets.Sets);
    }

    [Fact]
    public void ShouldChangeWorkoutRepetitionsWhenRepetitionsIsValid()
    {
        byte newRepetitions = 15;
        var workoutExercise = new WorkoutExercise(1, 1, _validSets, _validRepetitions);
        
        workoutExercise.UpdateRepetitions(newRepetitions);
        
        Assert.Equal(newRepetitions, workoutExercise.Repetitions!.Repetitions);
    }

    [Fact]
    public void ShouldNotChangeWorkoutRepetitionsWhenRepetitionsIsInvalid()
    {
        byte newRepetitions = 255;
        var workoutExercise = new WorkoutExercise(1, 1, _validSets, _validRepetitions);
        var currentRepetitions = workoutExercise.Repetitions!.Repetitions;
        
        workoutExercise.UpdateRepetitions(newRepetitions);
        
        Assert.Equal(currentRepetitions, workoutExercise.Repetitions!.Repetitions);
    }

    [Fact]
    public void ShouldChangeExerciseWeightWhenWeightIsValid()
    {
        var newWeight = 15f;
        var workoutExercise = new WorkoutExercise(1, 1, _validSets, _validRepetitions, _validWeight);
        
        workoutExercise.UpdateWeight(newWeight);
        
        Assert.Equal(newWeight, workoutExercise.Weight!.Weight);
    }

    [Fact]
    public void ShouldNotChangeExerciseWeightWhenWeightIsNotValid()
    {
        var invalidWeight = 200f;
        var workoutExercise = new WorkoutExercise(1, 1, _validSets, _validRepetitions, _validWeight);
        var currentWeight = workoutExercise.Weight!.Weight;
        
        workoutExercise.UpdateWeight(invalidWeight);
        
        Assert.Equal(currentWeight, workoutExercise.Weight.Weight);
    }

    [Fact]
    public void ShouldChangeExerciseTimeWhenDurationIsValid()
    {
        var newDuration = 180;
        var workoutExercise = new WorkoutExercise(1, 1, _validSets, _validDuration);
        
        workoutExercise.UpdateDuration(newDuration);
        
        Assert.Equal(newDuration, workoutExercise.Duration!.TimeInSeconds);
    }

    [Fact]
    public void ShouldNotChangeExerciseTimeWhenDurationIsInvalid()
    {
        var invalidDuration = 361;
        var workoutExercise = new WorkoutExercise(1, 1, _validSets, _validDuration);
        var currentDuration = workoutExercise.Duration!.TimeInSeconds;
        
        workoutExercise.UpdateDuration(invalidDuration);
        
        Assert.Equal(currentDuration, workoutExercise.Duration.TimeInSeconds);
    }
}