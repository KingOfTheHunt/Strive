using Strive.Core.Entities;

namespace Strive.Application.Users.UseCases.ChangeName.Contracts;

public interface IRepository
{
    Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    Task SaveAsync(User user, CancellationToken cancellationToken);
}