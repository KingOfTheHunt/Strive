using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Users.UseCases.Verify;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "email", "Informe um e-mail válido.")
            .AreEquals(request.VerificationCode.Length, 6, "verificationCode",
                "Informe um código de verificação válido.");
}