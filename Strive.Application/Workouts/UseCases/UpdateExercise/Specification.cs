using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Workouts.UseCases.UpdateExercise;

public static class Specification
{
    public static Contract<Notification> Assert(Request request)
    {
        var contract = new Contract<Notification>()
            .Requires()
            .IsGreaterThan(request.WorkoutId, 0, "workoutId",
                "O Id do treino é inválido.")
            .IsGreaterThan(request.ExerciseId, 0, "exerciseId",
                "O Id do exercício não é válido.")
            .IsGreaterOrEqualsThan(request.Sets, 3, "sets",
                "O número de séries deve ser maior ou igual a 3.");

        if (request.Repetitions is not null)
            contract.IsGreaterOrEqualsThan(request.Repetitions.Value, 10, "repetitions",
                "O número de repetições deve ser maior ou igual a 10.");

        if (request.Weight is not null)
            contract.IsGreaterOrEqualsThan(request.Weight.Value, 1, "weight",
                "O peso deve maior ou igual a 1.");

        if (request.Duration is not null)
            contract.IsGreaterThan(request.Duration.Value, 0, "duration",
                "O tempo deve ser maior do que 0.");

        return contract;
    }
}