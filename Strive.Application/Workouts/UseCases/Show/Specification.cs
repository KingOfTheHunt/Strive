using Flunt.Validations;
using Flunt.Notifications;

namespace Strive.Application.Workouts.UseCases.Show;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsGreaterThan(request.UserId, 0, "userId", "Informe um id válido.");
}