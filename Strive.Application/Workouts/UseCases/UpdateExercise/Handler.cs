using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Workouts.UseCases.UpdateExercise.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Application.Workouts.UseCases.UpdateExercise;

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
            return new Response(false, "Houve um erro inesperado com o banco de dados.", 500);
        }

        if (workout is null)
            return new Response(false, "Não há nenhum treino com este Id.", 404);

        if (workout.WorkoutExercises.Any(x => x.ExerciseId == request.ExerciseId) == false)
            return new Response(false, "Não há nenhum exercício com este Id neste treino.", 
                404);
        
        var exerciseId = request.ExerciseId;
        var sets = request.Sets;
        var repetitions = request.Repetitions;
        var weight = request.Weight;
        var duration = request.Duration;

        workout.UpdateExerciseSets(exerciseId, sets);
        workout.UpdateExerciseRepetitions(exerciseId, repetitions);
        workout.UpdateExerciseWeight(exerciseId, weight);
        workout.UpdateExerciseDuration(exerciseId, duration);

        if (!workout.IsValid)
            return new Response(false, "Não foi possível atualizar os dados do exercício.", 
                400, workout.Notifications);

        try
        {
            await repository.SaveAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, "Houve um erro na hora de salvar as alterações do treino.", 
                500);
        }

        logger.LogInformation("Treino de Id {workoutId} atualizado com sucesso.", workout.Id);
        return new Response(true, "Exercício atualizado com sucesso!", 200);
    }
}