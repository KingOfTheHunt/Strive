using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Users.UseCases.Details.Contracts;

namespace Strive.Application.Users.UseCases.Details;

public class Handler(IRepository repository, ILogger<Handler> logger) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var contract = Specification.Assert(request);

        if (!contract.IsValid)
            return new Response(false, "Dados inválidos.", 400, contract.Notifications);

        ResponseData? data;
        
        try
        {
            data = await repository.GetUserDataByIdAsync(request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Houve um erro na hora de buscar os dados no banco.");
            return new Response(false, "Houve um erro com o banco.", 500);
        }

        if (data is null)
            return new Response(false, "Não há nenhum usuário com este Id", 404);

        return new Response(true, "Dados obtidos com sucesso!", 200, data);
    }
}