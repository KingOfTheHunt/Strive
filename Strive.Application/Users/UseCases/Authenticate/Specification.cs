using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Users.UseCases.Authenticate;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "email", "Informe um e-mail válido.")
            .IsNotNullOrEmpty(request.Password, "password", "Informe uma senha.")
            .IsGreaterOrEqualsThan(request.Password.Length, 8, "password",
                "A senha precisa de no mínimo 8 caracteres.");
}