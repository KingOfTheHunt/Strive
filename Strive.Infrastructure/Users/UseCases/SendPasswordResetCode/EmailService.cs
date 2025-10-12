using Strive.Application.Users.UseCases.SendPasswordResetCode.Contracts;
using Strive.Core;
using Strive.Core.Entities;
using Strive.Infrastructure.Helpers;

namespace Strive.Infrastructure.Users.UseCases.SendPasswordResetCode;

public class EmailService : IEmailService
{
    public async Task SendResetPasswordCodeEmail(User user, CancellationToken cancellationToken)
    {
        var from = Configuration.Smtp.Login;
        var to = user.Email.Address;
        var username = user.Name.ToString();
        var subject = "Código de recuperação de senha";
        var body = $"""
                    Olá, {username}!<br/>
                    Aqui está o seu código para recuperar a sua senha: {user.Password.ResetCode}
                    """;

        var message = MailHelper.CreateMimeMessage(from, to, username, subject, body);
        await MailHelper.SendEmailAsync(message, cancellationToken);
    }
}