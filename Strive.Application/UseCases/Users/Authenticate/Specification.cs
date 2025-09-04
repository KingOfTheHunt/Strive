using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.UseCases.Users.Authenticate;

public static class Specification
{
    public static Contract<Notification> Assert(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "Email", "Informe um e-mail válido.")
            .IsNotNullOrEmpty(request.Password, "Password", "Informe a senha.");
}