using Flunt.Validations;
using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class WorkoutRepetitions : ValueObject
{
    public byte? Repetitions { get; private set; }
    
    protected WorkoutRepetitions() {}

    public WorkoutRepetitions(byte repetitions)
    {
        AddNotifications(new Contract<WorkoutRepetitions>()
            .Requires()
            .IsGreaterOrEqualsThan(repetitions, 5, nameof(repetitions),
            "O número de repetições precisa ser maior ou igual a 5.")
            .IsLowerOrEqualsThan(repetitions, 20, nameof(repetitions),
                "O número de repetições precisa ser menor ou igual a 20."));

        if (!IsValid)
            return;

        Repetitions = repetitions;
    }
}