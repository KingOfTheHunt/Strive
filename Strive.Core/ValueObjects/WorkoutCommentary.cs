using Flunt.Validations;
using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class WorkoutCommentary : ValueObject
{
    public string Commentary { get; private set; } = string.Empty;
    
    protected WorkoutCommentary() {}

    public WorkoutCommentary(string commentary)
    {
        AddNotifications(new Contract<WorkoutCommentary>()
            .Requires()
            .IsNotNullOrEmpty(commentary, nameof(commentary), 
                "O comentário não pode ser nulo ou vazio.")
            .IsGreaterOrEqualsThan(commentary.Length, 3, nameof(commentary),
                "O comentário precisa ter mais do que 3 letras.")
            .IsLowerOrEqualsThan(commentary.Length, 200, nameof(commentary), 
                "O comentário não pode ter mais de 200 letras."));

        if (!IsValid)
            return;

        Commentary = commentary;
    }
}