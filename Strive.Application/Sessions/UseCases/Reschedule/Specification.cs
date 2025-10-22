using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Sessions.UseCases.Reschedule;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsGreaterThan(request.WorkoutSessionId, 0, "workoutSessionId",
                "Informe um Id válido para a sessão de treino.")
            .IsGreaterThan(request.UserId, 0, "userId",
                "Informe um Id válido para o usuário.")
            .IsGreaterThan(request.ScheduleDate.ToUniversalTime(), DateTime.UtcNow,
                "scheduleDate", "Informe uma data válida para o agendamento.");
}