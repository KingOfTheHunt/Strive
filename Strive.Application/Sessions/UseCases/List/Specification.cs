using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Sessions.UseCases.List;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) => new Contract<Notification>()
        .Requires()
        .IsGreaterThan(request.UserId, 0, "userId", "Informe um userId válido.")
        .IsGreaterThan(request.EndDate, request.StartDate, "endDate",
            "A data final precisa ser maior do que a data inicial.");
}