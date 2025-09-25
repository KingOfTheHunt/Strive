using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class Verification : ValueObject
{
    public string Code { get; private set; } = Guid.NewGuid().ToString("N")[..6];
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddHours(5);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsVerified => ExpiresAt == null && VerifiedAt != null;

    public void Verify(string code)
    {
        if (IsVerified)
        {
            AddNotification(nameof(IsVerified), "A conta já foi verificada.");
            return;
        }

        if (DateTime.UtcNow > ExpiresAt)
        {
            AddNotification(nameof(ExpiresAt), "O código de verificação expirou.");
            return;
        }

        if (string.Equals(code.Trim(), Code, StringComparison.InvariantCultureIgnoreCase) == false)
        {
            AddNotification(nameof(Code), "O codigo informado é inválido.");
            return;
        }

        ExpiresAt = null;
        VerifiedAt = DateTime.UtcNow;
    }
}