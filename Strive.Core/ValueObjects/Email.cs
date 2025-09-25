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
            .IsEmailOrEmpty(address, "email", "O e-mail precisa ser informado."));

        if (!IsValid)
            return;

        Address = address;
        Verification = new Verification();
    }

    public override string ToString() => Address.ToLower();

    public static implicit operator string(Email email) => email.ToString();
    public static implicit operator Email(string address) => new Email(address);
}