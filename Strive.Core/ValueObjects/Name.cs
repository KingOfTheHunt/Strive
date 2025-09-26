using Flunt.Validations;
using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class Name : ValueObject
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;

    protected Name() {}
    
    public Name(string firstName, string lastName)
    {
        AddNotifications(new Contract<Name>()
            .Requires()
            .IsNotNullOrEmpty(firstName, nameof(firstName), "O nome precisa ser informado.")
            .IsGreaterOrEqualsThan(firstName.Length, 3, nameof(firstName),
                "O nome deve ter 3 ou mais letras.")
            .IsLowerOrEqualsThan(firstName.Length, 20, nameof(firstName),
                "O nome deve ter no máximo 20 letras.")
            .IsNotNullOrEmpty(lastName, nameof(lastName), "O sobrenome precisa ser informado.")
            .IsGreaterOrEqualsThan(lastName.Length, 3, nameof(lastName), 
                "O sobrenome precisa ter 3 ou mais letras.")
            .IsLowerOrEqualsThan(lastName.Length, 30, nameof(lastName), 
                "O sobrenome deve ter no máximo 30 letras."));

        if (!IsValid) return;
        FirstName = firstName;
        LastName = lastName;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}