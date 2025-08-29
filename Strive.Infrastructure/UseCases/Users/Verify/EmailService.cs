using Strive.Application.UseCases.Users.Verify.Contracts;
using Strive.Core;
using Strive.Core.Entities;
using Strive.Infrastructure.Helpers;

namespace Strive.Infrastructure.UseCases.Users.Verify;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(User user, CancellationToken cancellationToken = default)
    {
        var from = Configuration.Smtp.Login;
        var to = user.Email.Address;
        var userName = user.Name.ToString();
        var subject = "Verificação da conta no Strive";
        var body = $"""
                    Olá, {user.Name}! <br>
                    A sua conta foi verificada com sucesso!!! <br>
                    Aproveite tudo o que a sua conta pode oferecer. 
                    """;

        var message = MailHelper.CreateMailMessage(from, to, userName, subject, body);
        await MailHelper.SendEmailAsync(message, cancellationToken);
    }
}