using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Workouts.UseCases.RemoveExercise;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsGreaterThan(request.WorkoutId, 0, "workoutId",
                "O workoutId precisa ser maior do que 0.")
            .IsGreaterThan(request.ExerciseId, 0, "exerciseId",
                "O exerciseId precisa ser maior do que 0.");
}