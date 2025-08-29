using Strive.Core.Entities;

namespace Strive.Application.UseCases.Users.Verify.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task SaveAsync(User user, CancellationToken cancellationToken);
}