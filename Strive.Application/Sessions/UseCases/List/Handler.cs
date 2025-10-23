using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Sessions.UseCases.List.Contracts;

namespace Strive.Application.Sessions.UseCases.List;

public class Handler(IRepository repository, ILogger<Handler> logger) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var contract = Specification.Assert(request);

        if (!contract.IsValid)
            return new Response(false, "Dados inválidos.", 400, contract.Notifications);

        try
        {
            var startDateUtc = request.StartDate.ToUniversalTime();
            var endDateUtc = request.EndDate.ToUniversalTime();
            var data = await repository.GetScheduleWorkoutsByDate(request.UserId,
                startDateUtc, endDateUtc, cancellationToken);

            logger.LogInformation("Treinos agendados no intervalo entre {startDate} e {endDate}", request.StartDate,
                request.EndDate);
            return new Response(true, "Treinos agendados neste intervalo.", 200, data);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, "Houve um problema com o banco de dados.", 500);
        }
    }
}