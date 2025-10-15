using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Workouts.UseCases.Create.Contracts;
using Strive.Core.Entities;

namespace Strive.Application.Workouts.UseCases.Create;

public class Handler(IRepository repository, ILogger<Handler> logger) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var contract = Specification.Assert(request);

        if (!contract.IsValid)
            return new Response(false, "Dados inválidos.", 400, contract.Notifications);

        try
        {
            var workoutAlreadyExists = await repository.AnyWorkoutAsync(request.Name, cancellationToken);

            if (workoutAlreadyExists)
                return new Response(false, "Já existe um treino com este nome.", 400);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, "Houve inesperado com o banco de dados.", 500);
        }

        var workout = new Workout(request.Name, request.UserId);

        try
        {
            await repository.SaveAsync(workout, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, "Houve um erro inesperado com o banco de dados.", 500);
        }
        
        logger.LogInformation("Treino de ID {workoutId} criado com sucesso!", workout.Id);
        var data = new ResponseData { WorkoutId = workout.Id };
        return new Response(true, "Treino criado com sucesso!", 201, data);
    }
}