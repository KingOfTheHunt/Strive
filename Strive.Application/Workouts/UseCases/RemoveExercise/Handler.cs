using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Workouts.UseCases.RemoveExercise.Contracts;
using Strive.Core.Entities;

namespace Strive.Application.Workouts.UseCases.RemoveExercise;

public class Handler(IRepository repository, ILogger<Handler> logger) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken)
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
            return new Response(false, "Houve um erro no banco de dados.", 500);
        }

        if (workout is null)
            return new Response(false, "Não há nenhum treino com este Id", 404);
        
        workout.RemoveExercise(request.ExerciseId);

        if (!workout.IsValid)
            return new Response(false, "Não foi possível remover o exercício do treino",
                400, workout.Notifications);

        try
        {
            await repository.SaveAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, "Houve um problema na hora de atualizar os dados no banco.",
                500);
        }
        
        logger.LogInformation("O exercício de ID {exerciseId} foi removido do treino de ID {workoutId}", 
            request.ExerciseId, request.WorkoutId);
        return new Response(true, "Exercício removido com sucesso!", 200);
    }
}