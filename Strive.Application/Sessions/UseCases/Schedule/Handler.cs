using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Sessions.UseCases.Schedule.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Application.Sessions.UseCases.Schedule;

public class Handler(IRepository repository, ILogger<Handler> logger) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var contract = Specification.Assert(request);

        if (!contract.IsValid)
            return new Response(false, "Dados inválidos.", 400, contract.Notifications);

        try
        {
            var workoutExists = await repository.AnyWorkoutAsync(request.WorkoutId, request.UserId, cancellationToken);
            
            if (!workoutExists)
                return new Response(false, "Não há nenhum treino com este Id.", 404);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, "Houve inesperado no banco de dados.", 500);
        }
        
        var scheduleDate = new ScheduledAt(request.ScheduleDate);
        var workoutSession = new WorkoutSession(request.WorkoutId, scheduleDate);

        if (!workoutSession.IsValid)
            return new Response(false, "Dados inválidos.", 400, workoutSession.Notifications);

        try
        {
            await repository.SaveAsync(workoutSession, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, "Houve um erro na hora de agendar o treino.", 500);
        }
        
        logger.LogInformation("Treino de Id {workoutId} foi agendado com sucesso.", request.WorkoutId);
        return new Response(true, "Treino agendado com sucesso!", 200);
    }
}