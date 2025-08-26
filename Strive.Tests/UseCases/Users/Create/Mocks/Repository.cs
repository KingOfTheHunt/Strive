using Strive.Application.UseCases.Users.Create.Contracts;
using Strive.Core.Entities;

namespace Strive.Tests.UseCases.Users.Create.Mocks;

public class Repository : IRepository
{
    public Task<bool> AnyEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(email == "test@email.com");
    }

    public Task SaveAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}