using Microsoft.EntityFrameworkCore;
using Strive.Application.Users.UseCases.Verify.Contracts;
using Strive.Core.Entities;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Users.UseCases.Verify;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro na hora de acessar o banco.", ex);
        }
    }

    public async Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            context.Users.Update(user);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Houve um erro na hora de atualizar os dados do usuário.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro inesperado.", ex);
        }
    }
}