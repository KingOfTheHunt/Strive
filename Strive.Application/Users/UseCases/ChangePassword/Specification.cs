using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Users.UseCases.ChangePassword;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .IsGreaterThan(request.Id, 0, "Id", "Informe um Id válido.")
            .AreEquals(request.NewPassword, request.NewPasswordAgain, "newPassword",
                "As senha não são iguais.");
}