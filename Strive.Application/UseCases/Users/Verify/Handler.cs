using MedTheMediator.Abstractions;
using Strive.Application.UseCases.Users.Verify.Contracts;

namespace Strive.Application.UseCases.Users.Verify;

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
            return new Response(false, "O usuário não foi encontrado.", 404);
        
        user.Email.Verification.Verify(request.VerificationCode);

        if (!user.Email.Verification.IsValid)
            return new Response(false, "Não foi possível verificar a sua conta.", 400,
                user.Email.Verification.Notifications);

        await repository.SaveAsync(user, cancellationToken);
        await emailService.SendEmailAsync(user, cancellationToken);

        return new Response(true, "Conta verificada com sucesso!", 200);
    }
}