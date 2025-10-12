using Microsoft.EntityFrameworkCore;
using Strive.Application.Users.UseCases.SendPasswordResetCode.Contracts;
using Strive.Core.Entities;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Users.UseCases.SendPasswordResetCode;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Address == email,
                cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro inesperado com o banco.", ex);
        }
    }
}