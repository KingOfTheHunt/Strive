using Flunt.Validations;
using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class ExerciseWeight : ValueObject
{
    public float Weight { get; private set; }
    
    protected ExerciseWeight() {}

    public ExerciseWeight(float weight)
    {
        AddNotifications(new Contract<ExerciseWeight>()
            .Requires()
            .IsGreaterThan(weight, 0, nameof(weight), 
                "O peso deve ser maior do que 0kg.")
            .IsLowerOrEqualsThan(weight, 170, nameof(weight), 
                "O peso deve ser menor do que 170kg"));

        if (!IsValid)
            return;

        Weight = weight;
    }
}