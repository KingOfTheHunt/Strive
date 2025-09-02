using Strive.Application.UseCases.Users.ResendVerification.Contracts;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Tests.UseCases.Users.ResendVerification.Mocks;

public class Repository : IRepository
{
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var name = new Name("John", "Doe");
        var userEmail = new Email("test@email.com");
        var password = new Password("123456789");

        if (email == "test@email.com")
            return new User(name, userEmail, password);

        return null;
    }

    public Task SaveAsync(User user, CancellationToken cancellationToken) => Task.CompletedTask;
}