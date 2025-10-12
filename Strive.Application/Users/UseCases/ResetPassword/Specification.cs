using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Users.UseCases.ResetPassword;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "email", "Informe um e-mail válido.")
            .AreEquals(request.NewPassword, request.NewPasswordAgain, "newPassword",
                "As senhas não são iguais.")
            .AreEquals(request.ResetCode.Length, 6, "resetCode", "Código inválido.");
}