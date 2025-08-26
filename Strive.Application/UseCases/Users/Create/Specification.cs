using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.UseCases.Users.Create;

public static class Specification
{
    public static Contract<Notification> Assert(Request request)
    {
        return new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.FirstName, nameof(request.FirstName),
                "O primeiro nome precisa ser informado.")
            .IsNotNullOrEmpty(request.LastName, nameof(request.LastName),
                "O último nome precisa ser informado.")
            .IsEmail(request.Email, nameof(request.Email),
                "Informe um e-mail válido.")
            .IsNotNullOrEmpty(request.Password, nameof(request.Password),
                "A senha precisa ser informada.");
    }
}