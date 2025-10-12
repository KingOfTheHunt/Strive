using Strive.Core.Entities;

namespace Strive.Application.Users.UseCases.SendPasswordResetCode.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}