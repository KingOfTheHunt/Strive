using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Workouts.UseCases.Schedule.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Application.Workouts.UseCases.Schedule;

public class Handler(IRepository repository, ILogger<Handler> logger) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var contract = Specification.Assert(request);

        if (!contract.IsValid)
            return new Response(false, "Dados inválidos.", 400, contract.Notifications);

        Workout? workout;

        try
        {
            workout = await repository.GetWorkoutByIdAsync(request.WorkoutId, request.UserId, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, "Houve inesperado no banco de dados.", 500);
        }

        if (workout is null)
            return new Response(false, "Não há nenhum treino com este Id.", 404);

        var scheduleDate = new ScheduledAt(request.ScheduleDate);
        var workoutSession = new WorkoutSession(workout.Id, scheduleDate);
        workout.AddWorkoutSession(workoutSession);

        if (!workout.IsValid)
            return new Response(false, "Dados inválidos.", 400, workout.Notifications);

        try
        {
            await repository.SaveAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, "Houve um erro na hora de agendar o treino.", 500);
        }
        
        logger.LogInformation("Treino de Id {workoutId} foi agendado com sucesso.", workout.Id);
        return new Response(true, "Treino agendado com sucesso!", 200);
    }
}