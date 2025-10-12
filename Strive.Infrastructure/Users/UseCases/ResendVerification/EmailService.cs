using Strive.Application.Users.UseCases.ResendVerification.Contracts;
using Strive.Core;
using Strive.Core.Entities;
using Strive.Infrastructure.Helpers;

namespace Strive.Infrastructure.Users.UseCases.ResendVerification;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(User user, CancellationToken cancellationToken)
    {
        var from = Configuration.Smtp.Login;
        var to = user.Email.Address;
        var username = user.ToString();
        var subject = "Reenvio do código de verificação do Strive.";
        var body = $"""
                    Olá, {username}!<br/>
                    Aqui está o seu novo código de verificação como você requisitou.<br/>
                    <strong>Código de verificação:</strong> {user.Email.Verification.Code} <br/>
                    Ele é válido até {user.Email.Verification.ExpiresAt!.Value.ToLocalTime()}.
                    """;
        var message = MailHelper.CreateMimeMessage(from, to, username!, subject, body);
        await MailHelper.SendEmailAsync(message, cancellationToken);
    }
}