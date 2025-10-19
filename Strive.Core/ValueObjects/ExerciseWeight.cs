using Flunt.Validations;
using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class ExerciseWeight : ValueObject
{
    public float? Weight { get; private set; }
    
    protected ExerciseWeight() {}

    public ExerciseWeight(float? weight)
    {
        if (weight.HasValue)
        {
            AddNotifications(new Contract<ExerciseWeight>()
                .Requires()
                .IsGreaterThan(weight.Value, 0, nameof(weight),
                    "O peso deve ser maior do que 0kg.")
                .IsLowerOrEqualsThan(weight.Value, 170, nameof(weight),
                    "O peso deve ser menor do que 170kg"));

            if (!IsValid)
                return;
        }
        
        Weight = weight;
    }
}