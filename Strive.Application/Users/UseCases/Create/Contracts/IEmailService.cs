using Strive.Core.Entities;

namespace Strive.Application.Users.UseCases.Create.Contracts;

public interface IEmailService
{
    Task SendWelcomeEmail(User user, CancellationToken cancellationToken);
}