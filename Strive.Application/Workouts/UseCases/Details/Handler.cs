using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Workouts.UseCases.Details.Contracts;

namespace Strive.Application.Workouts.UseCases.Details;

public class Handler(IRepository repository, ILogger<Handler> logger) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var contract = Specification.Assert(request);

        if (!contract.IsValid)
            return new Response(false, "Dados inválidos.", 400, contract.Notifications);

        try
        {
            var workoutExists = await repository.AnyWorkoutAsync(request.WorkoutId, request.UserId, 
                cancellationToken);

            if (!workoutExists)
                return new Response(false, "Não há um treino com este Id", 404);
            
            var data = await repository.GetExercisesAsync(request.WorkoutId,
                request.UserId, cancellationToken);

            return new Response(true, "Exercícios obtidos com sucesso.", 200, data);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, "Houve um problema com o banco de dados.", 500);
        }
    }
}