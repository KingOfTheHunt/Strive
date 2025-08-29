using Microsoft.EntityFrameworkCore;
using Strive.Application.UseCases.Users.Verify.Contracts;
using Strive.Core.Entities;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.UseCases.Users.Verify;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken) =>
        await context.Users.FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);

    public async Task SaveAsync(User user, CancellationToken cancellationToken = default)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);
    }
}