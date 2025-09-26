using Flunt.Validations;
using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class Email : ValueObject
{
    public string Address { get; private set; } = string.Empty;
    public Verification Verification { get; private set; } = null!;

    protected Email() {}
    
    public Email(string address)
    {
        AddNotifications(new Contract<Email>()
            .Requires()
            .IsNotNullOrEmpty(address, "email", "O e-mail precisa ser informado.")
            .IsEmail(address, "email", "O e-mail inválido."));

        if (!IsValid)
            return;

        Address = address;
        Verification = new Verification();
    }
    
    public static implicit operator Email(string address) => new Email(address);
}