using Microsoft.EntityFrameworkCore;
using Strive.Application.UseCases.Users.Authenticate.Contracts;
using Strive.Core.Entities;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.UseCases.Users.Authenticate;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken) => 
        await context.Users.FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);
}