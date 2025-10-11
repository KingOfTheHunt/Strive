using Strive.Core.Entities;

namespace Strive.Application.Users.UseCases.Authenticate.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}