using Strive.Core.Abstractions;
using Strive.Core.ValueObjects;

namespace Strive.Core.Entities;

public class WorkoutSession : Entity
{
    public WorkoutCommentary? Commentary { get; private set; }
    public ScheduledAt? ScheduledAt { get; private set; }
    public bool Done { get; private set; }
    public Workout Workout { get; private set; } = null!;
    public int WorkoutId { get; private set; }

    protected WorkoutSession() {}
    
    public WorkoutSession(int workoutId)
    {
        WorkoutId = workoutId;
    }

    public WorkoutSession(int workoutId, WorkoutCommentary commentary, ScheduledAt scheduledAt) 
    : this(workoutId)
    {
        AddNotifications(commentary, scheduledAt);

        if (!IsValid)
            return;

        Commentary = commentary;
        ScheduledAt = scheduledAt;
    }

    public WorkoutSession(int workoutId, WorkoutCommentary commentary) : this(workoutId)
    {
        AddNotifications(commentary);
        
        if (!IsValid)
            return;

        Commentary = commentary;
    }

    public WorkoutSession(int workoutId, ScheduledAt scheduledAt) : this(workoutId)
    {
        AddNotifications(scheduledAt);

        if (!IsValid)
            return;

        ScheduledAt = scheduledAt;
    }

    public void AddWorkoutCommentary(string commentary)
    {
        var newCommentary = new WorkoutCommentary(commentary);

        if (!newCommentary.IsValid)
        {
            AddNotifications(newCommentary);
            return;
        }

        Commentary = newCommentary;
    }

    public void ScheduleWorkout(DateTime date)
    {
        var scheduledAt = new ScheduledAt(date);

        if (!scheduledAt.IsValid)
        {
            AddNotifications(scheduledAt);
            return;
        }

        ScheduledAt = scheduledAt;
    }

    public void FinishWorkout() => Done = true;
}