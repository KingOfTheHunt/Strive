using Strive.Core.Abstractions;

namespace Strive.Core.ValueObjects;

public class Verification : ValueObject
{
    public string Code { get; private set; } = Guid.NewGuid().ToString("N")[..6];
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddHours(6);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsActive => ExpiresAt == null && VerifiedAt != null;

    public void Verify(string code)
    {
        if (IsActive)
        {
            AddNotification("IsActive", "A conta já foi verificada.");
            return;
        }

        if (ExpiresAt < DateTime.UtcNow)
        {
            AddNotification("ExpiresAt", "O código de verificação expirou");
            return;
        }

        if (string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase) == false)
        {
            AddNotification("Code", "O código informado está incorreto.");
            return;
        }

        ExpiresAt = null;
        VerifiedAt = DateTime.UtcNow;
    }
}