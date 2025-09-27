using Flunt.Validations;
using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class WorkoutSets : ValueObject
{
    public byte Sets { get; private set; }

    protected WorkoutSets()
    {
    }

    public WorkoutSets(byte sets)
    {
        AddNotifications(new Contract<WorkoutSets>()
            .Requires()
            .IsGreaterOrEqualsThan(sets, 3, nameof(sets), 
                "O número de séries precisa ser maior ou igual a 3.")
            .IsLowerOrEqualsThan(sets, 5, nameof(sets), 
                "O número de séries precisa ser menor ou igual a 5."));

        if (!IsValid)
            return;

        Sets = sets;
    }
}