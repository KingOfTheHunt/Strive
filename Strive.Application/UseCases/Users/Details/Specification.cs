using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.UseCases.Users.Details;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .IsGreaterThan(request.Id, 0, "Id", "Informe um Id válido.");
}