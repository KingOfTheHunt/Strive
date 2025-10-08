using Microsoft.EntityFrameworkCore;
using Strive.Application.Users.UseCases.Create.Contracts;
using Strive.Core.Entities;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Users.UseCases.Create;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<bool> AnyEmailAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Users.AsNoTracking().
                AnyAsync(x => x.Email.Address == email, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro ao acessar os dados no banco.", ex);
        }
    }

    public async Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Houve um erro ao salvar os dados no banco.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro inesperado com o banco.", ex);
        }
    }
}