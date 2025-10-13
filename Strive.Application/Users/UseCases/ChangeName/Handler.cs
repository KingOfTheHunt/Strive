using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Users.UseCases.ChangeName.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Application.Users.UseCases.ChangeName;

public class Handler(IRepository repository, ILogger<Handler> logger) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var contract = Specification.Assert(request);

        if (!contract.IsValid)
            return new Response(false, "Dados inválidos.", 400, contract.Notifications);

        User? user;

        try
        {
            user = await repository.GetUserByIdAsync(request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Houve um problema na hora de acessar o banco de dados.");
            return new Response(false, "Houve um problema com o banco de dados.", 500);
        }

        if (user is null)
            return new Response(false, "Não há nenhum usuário com este Id.", 404);
        
        user.ChangeName(new Name(request.FirstName, request.LastName));

        if (!user.IsValid)
            return new Response(false, "Dados inválidos.", 400, user.Notifications);

        try
        {
            await repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Houve um erro ao salvar os dados no banco de dados.");
            return new Response(false, "Houve um problema com o banco de dados.", 500);
        }
        
        logger.LogInformation("Usuário com Id {userId} alterou o nome com sucesso.", user.Id);
        return new Response(true, "Nome alterado com sucesso.", 200);
    }
}