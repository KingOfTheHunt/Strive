using Strive.Application.UseCases.Users.ResendVerification.Contracts;
using Strive.Core.Entities;

namespace Strive.Tests.UseCases.Users.ResendVerification.Mocks;

public class EmailService : IEmailService
{
    public Task SendEmailAsync(User user, CancellationToken cancellationToken) => Task.CompletedTask;
}