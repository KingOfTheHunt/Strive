using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Workouts.UseCases.AddExercise;

public static class Specification
{
    public static Contract<Notification> Assert(Request request)
    {
        var contract = new Contract<Notification>()
            .Requires()
            .IsGreaterThan(request.WorkoutId, 0, "workoutId",
                "Informe um workoutId válido.")
            .IsGreaterThan(request.ExerciseId, 0, "exerciseId",
                "Informe um exerciseId válido.")
            .IsGreaterThan(request.Sets, 0, "sets",
                "Informe um número de séries maior do que 0.");

        if (request.Repetitions is not null)
            contract.IsGreaterThan(request.Repetitions.Value, 0, "repetitions",
                "Informe um número de repetições maior do que 0");

        if (request.Weight is not null)
            contract.IsGreaterThan(request.Weight.Value, 0.0f, "weight",
                "Informe um peso maior do que 0.");

        if (request.Duration is not null)
            contract.IsGreaterThan(request.Duration.Value, 0, "duration",
                "Informe uma duração maior do que 0 segundos.");

        return new Contract<Notification>();
    }
}