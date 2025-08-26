using Strive.Application.UseCases.Users.Create.Contracts;
using Strive.Core;
using Strive.Core.Entities;
using Strive.Infrastructure.Helpers;

namespace Strive.Infrastructure.UseCases.Users.Create;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(User user, CancellationToken cancellationToken)
    {
        var from = Configuration.Smtp.Login;
        var to = user.Email.Address;
        var username = user.Name.ToString();
        var subject = "Bem vindo ao Strive!!!";
        var body = $"""
                    <strong>Bem vindo ao Strive {username}!</strong>

                    Para começar a usar a sua conta basta informar o código abaixo.
                    Código: {user.Email.Verification.Code}
                    """;
        var message = MailHelper.CreateMailMessage(from, to, username, subject, body);
        await MailHelper.SendEmailAsync(message, cancellationToken);
    }
}