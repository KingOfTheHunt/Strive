using Microsoft.EntityFrameworkCore;
using Strive.Application.Users.UseCases.ChangePassword.Contracts;
using Strive.Core.Entities;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Users.UseCases.ChangePassword;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);
    }
}