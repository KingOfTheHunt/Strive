using Microsoft.EntityFrameworkCore;
using Strive.Application.Users.UseCases.Authenticate.Contracts;
using Strive.Core.Entities;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Users.UseCases.Authenticate;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro ao acessar o banco de dados.", ex);
        }
    }
}