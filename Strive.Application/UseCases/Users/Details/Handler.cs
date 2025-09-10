using MedTheMediator.Abstractions;
using Strive.Application.UseCases.Users.Details.Contracts;

namespace Strive.Application.UseCases.Users.Details;

public class Handler(IRepository repository) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var contract = Specification.Assert(request);

        if (!contract.IsValid)
            return new Response(false, "Dados inválidos.", 400, contract.Notifications);

        var data = await repository.GetUserByIdAsync(request.Id, cancellationToken);

        if (data is null)
            return new Response(false, "Não foi encontrado nenhum usuário com esse Id.", 404);

        return new Response(true, "Usuário obtido com sucesso.", 200, data);
    }
}