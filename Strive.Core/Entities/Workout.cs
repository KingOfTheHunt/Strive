using Flunt.Validations;
using Strive.Core.Abstractions;

namespace Strive.Core.Entities;

public class Workout : Entity
{
    public string Name { get; private set; } = string.Empty;
    public User User { get; private set; } = null!;
    public int UserId { get; private set; }
    public IList<WorkoutExercise> WorkoutExercises { get; private set; } = [];
    public IList<WorkoutSession> WorkoutSessions { get; private set; } = [];
    
    protected Workout() {}

    public Workout(string name, int userId)
    {
        Validate(name);
        
        if (!IsValid)
            return;

        Name = name;
        UserId = userId;
    }

    public void ChangeWorkoutName(string name)
    {
        Validate(name);

        if (!IsValid)
            return;

        Name = name;
    }
    
    public void AddExercise(WorkoutExercise exercise)
    {
        var exerciseAlreadyExistsInWorkout = WorkoutExercises
            .Any(e => e.ExerciseId == exercise.ExerciseId);

        if (exerciseAlreadyExistsInWorkout)
        {
            AddNotification("exercise", "O exercício escolhido já faz parte do seu treino.");
            return;
        }

        if (!exercise.IsValid)
        {
            AddNotifications(exercise);
            return;
        }
        
        WorkoutExercises.Add(exercise);
    }

    public void RemoveExercise(int exerciseId)
    {
        var exercise = GetExerciseById(exerciseId);

        if (exercise is null)
        {
            AddNotification("exerciseId", "Não existe nenhum exercício com esse Id no seu treino.");
            return;
        }

        WorkoutExercises.Remove(exercise);
    }

    public void UpdateExerciseSets(int exerciseId, byte sets)
    {
        var exercise = GetExerciseById(exerciseId);

        if (exercise is null)
        {
            AddNotification("exerciseId", "Não existe nenhum exercício com esse Id no seu treino.");
            return;
        }
        
        exercise.UpdateSets(sets);
        AddNotifications(exercise);
    }

    public void UpdateExerciseRepetitions(int exerciseId, byte? repetitions)
    {
        var exercise = GetExerciseById(exerciseId);

        if (exercise is null)
        {
            AddNotification("exerciseId", "Não existe nenhum exercício com esse Id no seu treino.");
            return;
        }
        
        exercise.UpdateRepetitions(repetitions);
        AddNotifications(exercise);
    }

    public void UpdateExerciseWeight(int exerciseId, float? weight)
    {
        var exercise = GetExerciseById(exerciseId);

        if (exercise is null)
        {
            AddNotification("exerciseId", "Não existe nenhum exercício com esse Id no seu treino.");
            return;
        }
        
        exercise.UpdateWeight(weight);
        AddNotifications(exercise);
    }

    public void UpdateExerciseDuration(int exerciseId, int? duration)
    {
        var exercise = GetExerciseById(exerciseId);

        if (exercise is null)
        {
            AddNotification("exerciseId", "Não existe nenhum exercício com esse Id no seu treino.");
            return;
        }
        
        exercise.UpdateDuration(duration);
        AddNotifications(exercise);
    }

    public void AddWorkoutSession(WorkoutSession session)
    {
        if (!session.IsValid)
        {
            AddNotifications(session);
            return;
        }
        
        WorkoutSessions.Add(session);
    }

    public void RemoveWorkoutSession(int workoutSessionId)
    {
        var workoutSession = GetWorkoutSessionById(workoutSessionId);

        if (workoutSession is null)
        {
            AddNotification("workoutSession", "Não existe nenhum sessão de treino com esse Id.");
            return;
        }

        WorkoutSessions.Remove(workoutSession);
    }

    public void UpdateWorkoutSessionSchedule(int workoutSessionId, DateTime scheduleDate)
    {
        var workoutSession = GetWorkoutSessionById(workoutSessionId);

        if (workoutSession is null)
        {
            AddNotification("workoutSession", "Não existe nenhum sessão de treino com esse Id.");
            return;
        }
        
        workoutSession.ScheduleWorkout(scheduleDate);
        AddNotifications(workoutSession);
    }

    public void UpdateWorkoutCommentary(int workoutSessionId, string commentary)
    {
        var workoutSession = GetWorkoutSessionById(workoutSessionId);

        if (workoutSession is null)
        {
            AddNotification("workoutSession", "Não existe nenhum sessão de treino com esse Id.");
            return;
        }
        
        workoutSession.AddWorkoutCommentary(commentary);
        AddNotifications(workoutSession);
    }

    public void FinishWorkoutSession(int workoutSessionId)
    {
        var workoutSession = GetWorkoutSessionById(workoutSessionId);

        if (workoutSession is null)
        {
            AddNotification("workoutSession", "Não existe nenhum sessão de treino com esse Id.");
            return;
        }
        
        workoutSession.FinishWorkout();
    }

    private WorkoutExercise? GetExerciseById(int exerciseId) => 
        WorkoutExercises.FirstOrDefault(e => e.ExerciseId == exerciseId);

    private WorkoutSession? GetWorkoutSessionById(int workoutSessionId) =>
        WorkoutSessions.FirstOrDefault(x => x.Id == workoutSessionId);

    private void Validate(string workoutName)
    {
        AddNotifications(new Contract<Workout>()
            .Requires()
            .IsNotNullOrEmpty(workoutName, nameof(workoutName), "O treino precisa de um nome.")
            .IsGreaterOrEqualsThan(workoutName.Length, 3, nameof(workoutName), 
                "O nome do treino deve ter mais de 3 letras.")
            .IsLowerOrEqualsThan(workoutName.Length, 30,nameof(workoutName), 
                "O nome do treino não pode ter mais de 30 letras."));
    }
}