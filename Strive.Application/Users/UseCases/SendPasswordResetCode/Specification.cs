using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Users.UseCases.SendPasswordResetCode;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "email", "Informe um e-mail válido.");
}