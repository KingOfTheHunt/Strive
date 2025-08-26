using Strive.Core.Entities;

namespace Strive.Application.UseCases.Users.Create.Contracts;

public interface IRepository
{
    Task<bool> AnyEmailAsync(string email, CancellationToken cancellationToken = default);
    Task SaveAsync(User user, CancellationToken cancellationToken = default);
}