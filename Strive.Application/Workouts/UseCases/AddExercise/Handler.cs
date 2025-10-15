using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Workouts.UseCases.AddExercise.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Application.Workouts.UseCases.AddExercise;

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
            logger.LogError(ex, "Houve um erro ao buscar o treino de Id {workoutId} no banco.", 
                request.WorkoutId);
            return new Response(false, "Houve um erro no banco.", 500);
        }

        if (workout is null)
            return new Response(false, "Não há nenhum treino com este Id.", 404);

        try
        {
            var exerciseExists = await repository.AnyExerciseAsync(request.ExerciseId, cancellationToken);

            if (!exerciseExists)
                return new Response(false, "Não há nenhum exercício com este Id", 404);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, ex.Message, 500);
        }
        
        var sets = new WorkoutSets(request.Sets);
        var repetitions = request.Repetitions.HasValue ? new WorkoutRepetitions(request.Repetitions.Value) : null;
        var weight = request.Weight.HasValue ? new ExerciseWeight(request.Weight.Value) : null;
        var duration = request.Duration.HasValue ? new ExerciseTime(request.Duration.Value) : null;
        var workoutExercise = new WorkoutExercise(request.WorkoutId, request.ExerciseId, sets, repetitions,
            weight, duration);
        
        workout.AddExercise(workoutExercise);

        if (!workout.IsValid)
            return new Response(false, "Houve um erro ao adicionar o exercício ao treino.",
                400, workout.Notifications);

        try
        {
            await repository.SaveAsync(workout, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Houve um erro na hora de salvar as atualizações do treino.");
            return new Response(false, "Houve um erro ao salvar as alterações do treino.", 500);
        }
        
        logger.LogInformation("Treino com Id {workoutId} alterado com sucesso", workout.Id);
        return new Response(true, "Exercício adicionado com sucesso!", 200);
    }
}