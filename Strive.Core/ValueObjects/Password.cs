using Flunt.Validations;
using SecureIdentity.Password;
using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class Password : ValueObject
{
    public string Hash { get; private set; } =  string.Empty;
    public string ResetCode { get; private set; } = Guid.NewGuid().ToString("N")[..6];
    
    protected Password() {}

    public Password(string password)
    {
        AddNotifications(new Contract<Password>()
            .Requires()
            .IsNotNullOrEmpty(password, nameof(password), "A senha precisa ser informada.")
            .IsGreaterOrEqualsThan(password.Length, 6, nameof(password), 
                "A senha deve ter no mínimo 6 caracteres.")
            .IsLowerOrEqualsThan(password.Length, 20, nameof(password), 
                "A senha deve ter no máximo 20 caracteres."));

        if (!IsValid)
            return;

        Hash = PasswordHasher.Hash(password, privateKey: Configuration.PrivateKey);
    }
}