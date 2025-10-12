using Strive.Core.Entities;

namespace Strive.Application.Users.UseCases.ChangePassword.Contracts;

public interface IRepository
{
    Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    Task SaveAsync(User user, CancellationToken cancellationToken);
}