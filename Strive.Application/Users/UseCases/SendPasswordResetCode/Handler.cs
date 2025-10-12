using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Users.UseCases.SendPasswordResetCode.Contracts;
using Strive.Core.Entities;

namespace Strive.Application.Users.UseCases.SendPasswordResetCode;

public class Handler(IRepository repository, IEmailService emailService, ILogger<Handler> logger)
    : IHandler<Request, Response>
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
            logger.LogError(ex, "Houve um erro ao buscar o usuário");
            return new Response(false, "Houve um erro ao acessar o banco de dados.", 500);
        }

        if (user is null)
            return new Response(false, "Não há nenhum usuário com este e-mail.", 404);

        await emailService.SendResetPasswordCodeEmail(user, cancellationToken);

        logger.LogInformation("E-mail enviado com sucesso para {userEmail}", user.Email.Address);
        return new Response(true, "E-mail enviado com sucesso!", 200);
    }
}