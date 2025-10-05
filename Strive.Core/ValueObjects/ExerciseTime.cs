using Flunt.Validations;
using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class ExerciseTime : ValueObject
{
    public int? TimeInSeconds { get; private set; }
    
    protected ExerciseTime() {}

    public ExerciseTime(int timeInSeconds)
    {
        AddNotifications(new Contract<ExerciseTime>()
            .Requires()
            .IsGreaterOrEqualsThan(timeInSeconds, 10, nameof(timeInSeconds),
                "O tempo do exercício não pode ser menor do que 10 segundos.")
            .IsLowerOrEqualsThan(timeInSeconds, 360, nameof(timeInSeconds),
                "O tempo do exercício não pode ultrapassar 360 segundos."));

        if (!IsValid)
            return;

        TimeInSeconds = timeInSeconds;
    }
}