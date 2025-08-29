using Flunt.Validations;
using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class Email : ValueObject
{
    public string Address { get; init; } =  string.Empty;
    public Verification Verification { get; private set; } = null!;
    
    protected Email() {}
    
    public Email(string address)
    {
        AddNotifications(new Contract<Email>()
            .Requires()
            .IsNotNullOrEmpty(address, nameof(Address), "Informe o endereço de e-mail.")
            .IsEmail(address, nameof(address), "Endereço de e-mail inválido."));
        Verification = new Verification();
        AddNotifications(Verification);

        if (!IsValid)
            return;
        
        Address = address;
    }

    public void ResendVerification() => Verification = new Verification();
}