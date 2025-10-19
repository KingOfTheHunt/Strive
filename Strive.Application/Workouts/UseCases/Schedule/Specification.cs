using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Workouts.UseCases.Schedule;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsGreaterThan(request.WorkoutId, 0, "workoutId",
                "Informe um Id válido para o treino.")
            .IsGreaterThan(request.ScheduleDate.ToUniversalTime(), DateTime.UtcNow, "scheduleData",
                "Informe uma data válida.");
}