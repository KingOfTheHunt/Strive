using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Users.UseCases.Details;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsGreaterThan(request.Id, 0, "Informe um Id válido.");
}