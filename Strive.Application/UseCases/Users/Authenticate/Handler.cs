using MedTheMediator.Abstractions;
using Strive.Application.UseCases.Users.Authenticate.Contracts;

namespace Strive.Application.UseCases.Users.Authenticate;

public class Handler(IRepository repository) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var contract = Specification.Assert(request);

        if (!contract.IsValid)
            return new Response(false, "Dados inválidos", 400, contract.Notifications);

        var user = await repository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is null)
            return new Response(false, "Não foi encontrado nenhum usuário com este e-mail.", 404);

        if (!user.Password.Challenge(request.Password))
            return new Response(false, "Senha inválida.", 400);

        var data = new ResponseData(user.Id, user.Email.Address, user.Name.ToString(), string.Empty);

        return new Response(true, "Autenticado com sucesso!", 200, data);
    }
}