using Strive.Core.Entities;

namespace Strive.Application.Users.UseCases.ResendVerification.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task SaveAsync(User user, CancellationToken cancellationToken);
}