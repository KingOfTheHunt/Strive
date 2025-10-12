using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Users.UseCases.ChangePassword.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Application.Users.UseCases.ChangePassword;

public class Handler(IRepository repository, ILogger<Handler> logger) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = new CancellationToken())
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
            logger.LogError(ex, "Houve um problema no banco de dados ao buscar o usuário com {userId}.", 
                request.Id);
            return new Response(false, "Houve um problema com o banco de dados.", 500);
        }

        if (user is null)
            return new Response(false, "Não há nenhuma conta com este Id.", 404);
        
        user.ChangePassword(new Password(request.NewPassword));

        if (!user.IsValid)
            return new Response(false, "Informe uma senha válida.", 400);

        try
        {
            await repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Houve um problema na hora de salvar os dados no banco.");
            return new Response(false, "Houve um problema no banco de dados.", 500);
        }

        logger.LogInformation("Usuário com {userId} alterou a senha com sucesso!", user.Id);
        return new Response(true, "Senha alterada com sucesso!", 200);
    }
}