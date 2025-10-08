using MedTheMediator.Abstractions;
using Strive.Application.Users.UseCases.Create.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Application.Users.UseCases.Create;

public class Handler(IRepository repository, IEmailService emailService) : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = new())
    {
        var contract = Specification.Assert(request);

        if (!contract.IsValid)
            return new Response(false, "Dados inválidos.", 400, contract.Notifications);

        bool emailAlreadyExists;

        try
        {
            emailAlreadyExists = await repository.AnyEmailAsync(request.Email, cancellationToken);
        }
        catch (Exception ex)
        {
            return new Response(false, ex.Message, 500);
        }


        if (emailAlreadyExists)
            return new Response(false, "Uma conta já foi criada com esse e-mail.", 409);

        var name = new Name(request.FirstName, request.LastName);
        var email = new Email(request.Email);
        var password = new Password(request.Password);
        var user = new User(name, email, password);

        if (!user.IsValid)
            return new Response(false, "Dados inválidos.", 400, user.Notifications);

        try
        {
            await repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            return new Response(false, "Houve um erro inesperado.", 500);
        }
        
        await emailService.SendWelcomeEmail(user, cancellationToken);

        return new Response(true, "Conta criada com sucesso!", 201);
    }
}