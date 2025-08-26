using Strive.Application.UseCases.Users.Create.Contracts;
using Strive.Core.Entities;

namespace Strive.Tests.UseCases.Users.Create.Mocks;

public class EmailService : IEmailService
{
    public Task SendEmailAsync(User user, CancellationToken cancellationToken) => Task.CompletedTask;
}