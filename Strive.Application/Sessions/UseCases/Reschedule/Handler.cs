using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Sessions.UseCases.Reschedule.Contracts;
using Strive.Core.Entities;

namespace Strive.Application.Sessions.UseCases.Reschedule;

public class Handler(IRepository repository, ILogger<Handler> logger) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var contract = Specification.Assert(request);

        if (!contract.IsValid)
            return new Response(false, "Dados inválidos.", 400, contract.Notifications);

        WorkoutSession? session;

        try
        {
            session = await repository.GetWorkoutSessionByIdAsync(request.WorkoutSessionId, request.UserId,
                cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, "Houve um erro com o banco de dados.", 500);
        }

        if (session is null)
            return new Response(false, "Não há nenhuma sessão de treino com este Id.", 404);
        
        session.ScheduleWorkout(request.ScheduleDate.ToUniversalTime());

        if (!session.IsValid)
            return new Response(false, "Data inválida.", 400, session.Notifications);

        try
        {
            await repository.SaveAsync(session, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, "Houve um problema na hora de salvar os dados no banco de dados.",
                500);
        }
        
        logger.LogInformation("Treino de Id {workoutSessionId} reagendado com sucesso.", session.Id);
        return new Response(true, "Treino reagendado com sucesso!", 200);
    }
}