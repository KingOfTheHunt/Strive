using Strive.Core.Entities;

namespace Strive.Application.Users.UseCases.ResendVerification.Contracts;

public interface IEmailService
{
    Task SendEmailAsync(User user, CancellationToken cancellationToken);
}