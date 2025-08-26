using Microsoft.EntityFrameworkCore;
using Strive.Application.UseCases.Users.Create.Contracts;
using Strive.Core.Entities;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.UseCases.Users.Create;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<bool> AnyEmailAsync(string email, CancellationToken cancellationToken = default) => 
        await context.Users.AnyAsync(x => x.Email.Address == email, cancellationToken);

    public async Task SaveAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}