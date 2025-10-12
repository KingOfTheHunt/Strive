using MedTheMediator.Abstractions;
using Microsoft.Extensions.Logging;
using Strive.Application.Users.UseCases.ResendVerification.Contracts;
using Strive.Core.Entities;

namespace Strive.Application.Users.UseCases.ResendVerification;

public class Handler(IRepository repository, IEmailService emailService, ILogger<Handler> logger) 
    : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = new CancellationToken())
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
            logger.LogError(ex, ex.Message);
            return new Response(false, "Erro ao buscar os dados no banco.", 500);
        }

        if (user is null)
            return new Response(false, "Não há nenhuma conta com este e-mail", 404);


        if (user.Email.Verification.IsVerified)
            return new Response(false, "A conta já foi verificada.", 400);
        
        user.Email.CreateNewVerification();

        try
        {
            await repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return new Response(false, "Houve um problema com o banco.", 500);
        }

        await emailService.SendEmailAsync(user, cancellationToken);

        logger.LogInformation("E-mail enviado com sucesso para {email}", user.Email.Address);
        return new Response(true, "E-mail enviado com sucesso!", 200);
    }
}