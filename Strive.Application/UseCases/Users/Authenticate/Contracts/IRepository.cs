using Strive.Core.Entities;

namespace Strive.Application.UseCases.Users.Authenticate.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}