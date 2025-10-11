using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Users.UseCases.Authenticate.Contracts;
using Strive.Core.Entities;

namespace Strive.Application.Users.UseCases.Authenticate;

public class Handler(IRepository repository, ILogger<Handler> logger) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = new())
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
            logger.LogError(ex, "Erro ao acessar o banco de dados.");
            return new Response(false, "Erro ao acessar o banco de dados.", 500);
        }

        if (user is null)
            return new Response(false, "Usuário e/ou senha incorreto(s).", 400);

        if (!user.Password.Challenge(request.Password))
            return new Response(false, "Usuário e/ou senha incorreto(s).", 400);

        var data = new ResponseData
        {
            Id = user.Id,
            Name = user.Name.ToString()
        };

        logger.LogInformation("Usuário do ID {ID} foi autenticado", data.Id);
        return new Response(true, "Usuário autenticado com sucesso!", 200, data);
    }
}