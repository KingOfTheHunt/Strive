using Flunt.Validations;
using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class ExerciseTime : ValueObject
{
    public int TimeInSeconds { get; private set; }
    
    protected ExerciseTime() {}

    public ExerciseTime(int timeInSeconds)
    {
        AddNotifications(new Contract<ExerciseTime>()
            .Requires()
            .IsGreaterThan(timeInSeconds, 0, nameof(timeInSeconds),
                "O tempo do exercício não pode ser menor ou igual a 0."));

        if (!IsValid)
            return;

        TimeInSeconds = timeInSeconds;
    }
}