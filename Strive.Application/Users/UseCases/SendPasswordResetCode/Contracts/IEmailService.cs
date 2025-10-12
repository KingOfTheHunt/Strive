using Strive.Core.Entities;

namespace Strive.Application.Users.UseCases.SendPasswordResetCode.Contracts;

public interface IEmailService
{
    Task SendResetPasswordCodeEmail(User user, CancellationToken cancellationToken);
}