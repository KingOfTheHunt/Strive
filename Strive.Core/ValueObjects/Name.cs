using Flunt.Validations;
using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class Name : ValueObject
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;

    protected Name() {}
    
    public Name(string firstName, string lastName)
    {
        AddNotifications(new Contract<Name>()
            .Requires()
            .IsNotNullOrEmpty(firstName, nameof(firstName), "O primeiro nome precisa ser informado.")
            .IsNotNullOrEmpty(lastName, nameof(lastName), "O último nome precisa ser informado.")
            .IsGreaterOrEqualsThan(firstName.Length, 3, "O primeiro nome deve ter no mínimo 3 letras.")
            .IsLowerOrEqualsThan(firstName.Length, 20, "O primeiro nome deve ter no máximo 20 letras.")
            .IsGreaterOrEqualsThan(lastName.Length, 3, "O último nome deve ter no mínimo 3 letras.")
            .IsLowerOrEqualsThan(lastName.Length, 30, "O último nome deve ter no máximo 30 letras."));

        if (!IsValid) return;
        
        FirstName = firstName;
        LastName = lastName;
    }
}