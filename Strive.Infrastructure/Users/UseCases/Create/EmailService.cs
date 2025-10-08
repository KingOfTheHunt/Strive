using Strive.Application.Users.UseCases.Create.Contracts;
using Strive.Core;
using Strive.Core.Entities;
using Strive.Infrastructure.Helpers;

namespace Strive.Infrastructure.Users.UseCases.Create;

public class EmailService : IEmailService
{
    public async Task SendWelcomeEmail(User user, CancellationToken cancellationToken)
    {
        var from = Configuration.Smtp.Login;
        var to = user.Email.Address;
        var userName = user.Name.ToString();
        var subject = "Bem vindo ao Strive";
        var body = $"""
                    Olá, {userName}! <br/>
                    Seja bem vindo ao Strive! <br/>
                    É necessário ativar a sua conta e para fazer isso basta informar o código abaixo.<br/>
                    <strong>Código de verificação:</strong> {user.Email.Verification.Code}<br/>
                    Esse código é válido até as {user.Email.Verification.ExpiresAt!.Value.ToLocalTime()}
                    """;
        var message = MailHelper.CrateMimeMessage(from, to, userName, subject, body);
        await MailHelper.SendEmailAsync(message, cancellationToken);
    }
}