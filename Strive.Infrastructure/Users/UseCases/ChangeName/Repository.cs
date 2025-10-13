using Microsoft.EntityFrameworkCore;
using Strive.Application.Users.UseCases.ChangeName.Contracts;
using Strive.Core.Entities;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Users.UseCases.ChangeName;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um problema com o banco de dados.", ex);
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
            throw new Exception("Houve um erro na hora de salvar os dados no banco.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro inesperado.", ex);
        }
    }
}