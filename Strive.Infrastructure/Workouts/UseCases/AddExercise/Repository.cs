using Microsoft.EntityFrameworkCore;
using Strive.Application.Workouts.UseCases.AddExercise.Contracts;
using Strive.Core.Entities;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Workouts.UseCases.AddExercise;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<Workout?> GetWorkoutByIdAsync(int id, int userId, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Workouts.Include(x => x.WorkoutExercises)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro ao buscar os dados do treino no banco.", ex);
        }
    }

    public async Task<bool> AnyExerciseAsync(int exerciseId, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Exercises.AnyAsync(x => x.Id == exerciseId, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve erro ao verificar a existência do exercício.", ex);
        }
    }

    public async Task SaveAsync(Workout workout, CancellationToken cancellationToken)
    {
        try
        {
            context.Workouts.Update(workout);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Houve um problema na hora de atualizar o treino.", ex);
        }
    }
}