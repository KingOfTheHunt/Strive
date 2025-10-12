using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Users.UseCases.ResetPassword.Contracts;
using Strive.Core.Entities;

namespace Strive.Application.Users.UseCases.ResetPassword;

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
            user = await repository.GetUserByEmailAsync(request.Email, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Houve um erro ao obter o usuário no banco.");
            return new Response(false, "Houve um erro no banco.", 500);
        }

        if (user is null)
            return new Response(false, "Não há nenhuma conta com este e-mail.", 404);
        
        user.ResetPassword(request.NewPassword, request.ResetCode);

        if (!user.IsValid)
            return new Response(false, "O código informado está incorreto", 400);

        try
        {
            await repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
        
        logger.LogInformation("A senha do {userEmail} foi recuperada com sucesso!", request.Email);
        return new Response(true, "Senha alterada com sucesso.", 200);
    }
}