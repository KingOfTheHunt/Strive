using System.Text.RegularExpressions;
using Strive.Core.Abstractions;
using Flunt.Validations;

namespace Strive.Core.ValueObjects;

public class Password : ValueObject
{
    private const string RegexPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$";
    
    public string Hash { get; private set; } = string.Empty;
    public string ResetCode { get; private set; } = Guid.NewGuid().ToString("N")[..6];

    protected Password() {}
    
    public Password(string password)
    {
        AddNotifications(new Contract<Password>()
            .Requires()
            .IsNotNullOrEmpty(password, nameof(password), "A senha precisa ser informada.")
            .IsGreaterOrEqualsThan(password.Length, 8, nameof(password), 
                "A senha precisa ter no mínimo 8 caracteres.")
            .IsLowerOrEqualsThan(password.Length, 20, nameof(password), 
                "A senha deve ter no máximo 20 caracteres.")
            .IsTrue(SatisfiesPasswordPolicy(password), nameof(password), 
                """
                A senha precisa conter pelo menos um caracter maiúsculo, um caracter minúsculo, 
                um caracter minúsculo, um caracter especial e um número.
                """));

        if (!IsValid)
            return;

        Hash = BCrypt.Net.BCrypt.HashPassword(password);
    }
    
    public bool Challenge(string password) => BCrypt.Net.BCrypt.Verify(password, Hash);

    private bool SatisfiesPasswordPolicy(string password)
    {
        return Regex.IsMatch(password, RegexPattern);
    }
}