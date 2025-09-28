using Flunt.Validations;
using Strive.Core.Abstractions;
using Strive.Core.ValueObjects;

namespace Strive.Core.Entities;

public class WorkoutExercise : Entity
{
    public Workout Workout { get; private set; } = null!;
    public int WorkoutId { get; private set; }
    public Exercise Exercise { get; private set; } = null!;
    public int ExerciseId { get; private set; }
    public WorkoutSets Sets { get; private set; } = null!;
    public WorkoutRepetitions? Repetitions { get; private set; }
    public ExerciseWeight? Weight { get; private set; }
    public ExerciseTime? Duration { get; private set; }
    
    protected WorkoutExercise() {}

    private WorkoutExercise(int workoutId, int exerciseId, WorkoutSets sets)
    {
        WorkoutId = workoutId;
        ExerciseId = exerciseId;
        
        AddNotifications(new Contract<WorkoutExercise>()
            .Requires()
            .IsNotNull(sets, nameof(sets), "O número de séries precisa ser informado."));
        
        if (!IsValid)
            return;

        Sets = sets;
    }
    
    public WorkoutExercise(int workoutId, int exerciseId, WorkoutSets sets,
        WorkoutRepetitions repetitions, ExerciseWeight weight) : this(workoutId, exerciseId, sets)
    {
        AddNotifications(repetitions, weight);

        if (!IsValid)
            return;

        Repetitions = repetitions;
        Weight = weight;
    }

    public WorkoutExercise(int workoutId, int exerciseId, WorkoutSets sets, WorkoutRepetitions repetitions)
        : this(workoutId, exerciseId, sets)
    {
        AddNotifications(repetitions);

        if (!IsValid)
            return;
        
        Repetitions = repetitions;
    }

    public WorkoutExercise(int workoutId, int exerciseId, WorkoutSets sets, ExerciseTime duration)
        : this(workoutId, exerciseId, sets)
    {
        AddNotifications(duration);

        if (!IsValid)
            return;

        Duration = duration;
    }

    public void UpdateSets(byte newSets)
    {
        var sets = new WorkoutSets(newSets);

        if (!sets.IsValid)
        {
            AddNotifications(sets);
            return;
        }

        Sets = sets;
    }

    public void UpdateRepetitions(byte newRepetitions)
    {
        var repetitions = new WorkoutRepetitions(newRepetitions);

        if (!repetitions.IsValid)
        {
            AddNotifications(repetitions);
            return;
        }

        Repetitions = repetitions;
    }

    public void UpdateWeight(float newWeight)
    {
        var weight = new ExerciseWeight(newWeight);

        if (!weight.IsValid)
        {
            AddNotifications(weight);
            return;
        }

        Weight = weight;
    }

    public void UpdateDuration(int newDuration)
    {
        var duration = new ExerciseTime(newDuration);

        if (!duration.IsValid)
        {
            AddNotifications(duration);
            return;
        }

        Duration = duration;
    }
}