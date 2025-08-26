using Strive.Core.Entities;

namespace Strive.Application.UseCases.Users.Create.Contracts;

public interface IEmailService
{
    Task SendEmailAsync(User user, CancellationToken cancellationToken);
}