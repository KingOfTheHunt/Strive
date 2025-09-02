using Strive.Core.Entities;

namespace Strive.Application.UseCases.Users.ResendVerification.Contracts;

public interface IEmailService
{
    Task SendEmailAsync(User user, CancellationToken cancellationToken);
}