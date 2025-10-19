using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Workouts.UseCases.Show.Contracts;

namespace Strive.Application.Workouts.UseCases.Show;

public class Handler(IRepository repository, ILogger<Handler> logger) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var contract = Specification.Assert(request);

        if (!contract.IsValid)
            return new Response(false, "Dados inválidos.", 400, contract.Notifications);

        IEnumerable<string> workouts = [];

        try
        {
            workouts = await repository.GetAllWorkoutsAsync(request.UserId, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, "Houve um erro na hora de buscar os treinos no banco.", 
                500);
        }

        if (workouts.Any() == false)
            return new Response(false, "Não há nenhum treino cadastrado.", 404);
        
        var data = new ResponseData(workouts);
        logger.LogInformation("Exibindo os treino do usuário de Id {userId}", request.UserId);
        return new Response(true, "Treinos obtidos com sucesso.", 200, data);
    }
}