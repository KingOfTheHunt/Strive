using Microsoft.EntityFrameworkCore;
using Strive.Application.Workouts.UseCases.Create.Contracts;
using Strive.Core.Entities;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Workouts.UseCases.Create;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<bool> AnyWorkoutAsync(string name, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Workouts.AnyAsync(x => x.Name == name, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro inesperado com o banco de dados.", ex);
        }
    }

    public async Task SaveAsync(Workout workout, CancellationToken cancellationToken)
    {
        try
        {
            await context.Workouts.AddAsync(workout, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Houve um erro no banco na hora de salvar o treino.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro inesperado com o banco de dados.", ex);
        }
    }
}