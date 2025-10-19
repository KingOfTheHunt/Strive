using Microsoft.EntityFrameworkCore;
using Strive.Application.Workouts.UseCases.Schedule.Contracts;
using Strive.Core.Entities;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Workouts.UseCases.Schedule;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<Workout?> GetWorkoutByIdAsync(int workoutId, int userId, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Workouts.FirstOrDefaultAsync(x => x.Id == workoutId && x.UserId == userId,
                cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro ao buscar os dados do treino.", ex);
        }
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Houve um erro na hora de salvar os dados do agendamento no banco.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro inesperado no banco.", ex);
        }
    }
}