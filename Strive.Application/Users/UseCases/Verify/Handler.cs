using MedTheMediator.Abstractions;
using Strive.Application.Users.UseCases.Verify.Contracts;
using Strive.Core.Entities;

namespace Strive.Application.Users.UseCases.Verify;

public class Handler(IRepository repository) : IHandler<Request, Response>
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
            return new Response(false, ex.Message, 500);
        }

        if (user is null)
            return new Response(false, "Não há nenhum usuário com este e-mail.", 404);
        
        user.Email.Verification.Verify(request.VerificationCode);

        if (!user.Email.Verification.IsValid)
            return new Response(false, "Houve um problema ao verificar a conta.", 400,
                user.Email.Verification.Notifications);

        try
        {
            await repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception ex)
        {
            return new Response(false, ex.Message, 500);
        }

        return new Response(true, "Conta verificada com sucesso!", 200);
    }
}