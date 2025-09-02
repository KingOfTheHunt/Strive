using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.UseCases.Users.ResendVerification;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "Email", "O e-mail informado não é válido.");
}