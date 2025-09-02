using MedTheMediator.Abstractions;
using Strive.Application.UseCases.Users.ResendVerification.Contracts;

namespace Strive.Application.UseCases.Users.ResendVerification;

public class Handler(IRepository repository, IEmailService emailService) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var contract = Specification.Assert(request);

        if (!contract.IsValid)
            return new Response(false, "Os dados informados não são válidos.", 400,
                contract.Notifications);

        var user = await repository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is null)
            return new Response(false, "Não foi encontrado nenhum usuário com este e-mail no banco.",
                404);

        if (user.Email.Verification.IsActive)
            return new Response(false, "Esta conta já foi verificada.", 400);
        
        user.Email.ResendVerification();
        await repository.SaveAsync(user, cancellationToken);
        await emailService.SendEmailAsync(user, cancellationToken);

        return new Response(true, "Código gerado e enviado com sucesso!", 200);
    }
}