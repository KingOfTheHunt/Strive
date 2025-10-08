using Strive.Core.Entities;

namespace Strive.Application.Users.UseCases.Create.Contracts;

public interface IRepository
{
    Task<bool> AnyEmailAsync(string email, CancellationToken cancellationToken);
    Task SaveAsync(User user, CancellationToken cancellationToken);
}