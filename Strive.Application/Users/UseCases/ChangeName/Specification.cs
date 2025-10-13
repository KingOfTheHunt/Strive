using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Users.UseCases.ChangeName;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) => new Contract<Notification>()
        .Requires()
        .IsGreaterThan(request.Id, 0, "id", "Informe um id válido.")
        .IsNotNullOrEmpty(request.FirstName, "firstName", "O primeiro nome precisa ser informado.")
        .IsNotNullOrEmpty(request.LastName, "lastName", "O último nome precisa ser informado.");
}