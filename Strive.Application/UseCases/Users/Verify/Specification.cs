using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.UseCases.Users.Verify;

public static class Specification
{
    public static Contract<Notification> Assert(Request request)
    {
        return new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "Email", "O e-mail informado não é válido.")
            .AreEquals(request.VerificationCode.Length, 6, "VerificationCode",
                "O código de verificação não é válido.");
    }
}