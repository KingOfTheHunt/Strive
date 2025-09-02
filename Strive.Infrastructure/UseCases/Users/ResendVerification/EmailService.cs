using Strive.Application.UseCases.Users.ResendVerification.Contracts;
using Strive.Core;
using Strive.Core.Entities;
using Strive.Infrastructure.Helpers;

namespace Strive.Infrastructure.UseCases.Users.ResendVerification;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(User user, CancellationToken cancellationToken)
    {
        var from = Configuration.Smtp.Login;
        var to = user.Email.Address;
        var username = user.Name.ToString();
        var subject = "Reenvio do código de verificação";
        var body = $"""
                    Você pediu e aqui está o seu novo código de verificação. <br>
                    <strong>Código:</strong> {user.Email.Verification.Code}
                    """;
        var message = MailHelper.CreateMailMessage(to, from, username, subject, body);
        await MailHelper.SendEmailAsync(message, cancellationToken);
    }
}