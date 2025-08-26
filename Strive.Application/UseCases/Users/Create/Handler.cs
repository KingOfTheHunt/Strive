using MedTheMediator.Abstractions;
using Strive.Application.UseCases.Users.Create.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Application.UseCases.Users.Create;

public class Handler(IRepository repository, IEmailService service) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = new CancellationToken())
    {
        var contract = Specification.Assert(request);

        if (contract.IsValid == false)
            return new Response(false, "Os dados informados não são válidos.", 
                400, contract.Notifications);

        var emailAlreadyExists = await repository.AnyEmailAsync(request.Email, cancellationToken);
        
        if (emailAlreadyExists)
            return new Response(false, "O e-mail já está cadastrado em nossa base de dados.", 
                400);

        var name = new Name(request.FirstName, request.LastName);
        var email = new Email(request.Email);
        var password = new Password(request.Password);
        var user = new User(name, email, password);

        if (user.IsValid == false)
            return new Response(false, "Os dados informados não são válidos.",
                400, user.Notifications);

        await repository.SaveAsync(user, cancellationToken);
        await service.SendEmailAsync(user, cancellationToken);

        return new Response(true, "Conta criada com sucesso!", 201);
    }
}