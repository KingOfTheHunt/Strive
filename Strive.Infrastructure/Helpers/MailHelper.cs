using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Strive.Core;

namespace Strive.Infrastructure.Helpers;

public static class MailHelper
{
    public static MimeMessage CreateMimeMessage(string from, string to, string userName, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Strive", from));
        message.To.Add(new MailboxAddress(userName, to));
        message.Subject = subject;
        message.Body = new TextPart(TextFormat.Html)
        {
            Text = body
        };

        return message;
    }

    public static async Task SendEmailAsync(MimeMessage message, CancellationToken cancellationToken)
    {
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(Configuration.Smtp.Host, Configuration.Smtp.Port, SecureSocketOptions.Auto, 
            cancellationToken);
        await smtp.AuthenticateAsync(Configuration.Smtp.Login, Configuration.Smtp.Password, cancellationToken);
        await smtp.SendAsync(message, cancellationToken);
        await smtp.DisconnectAsync(true, cancellationToken);
    }
}