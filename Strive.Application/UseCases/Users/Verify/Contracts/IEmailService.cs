using Strive.Core.Entities;

namespace Strive.Application.UseCases.Users.Verify.Contracts;

public interface IEmailService
{
    Task SendEmailAsync(User user, CancellationToken cancellationToken = default);
}