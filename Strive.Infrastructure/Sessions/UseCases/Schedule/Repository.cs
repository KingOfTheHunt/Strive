using Microsoft.EntityFrameworkCore;
using Strive.Application.Sessions.UseCases.Schedule.Contracts;
using Strive.Core.Entities;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Sessions.UseCases.Schedule;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<bool> AnyWorkoutAsync(int workoutId, int userId, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Workouts.AnyAsync(x => x.Id == workoutId && x.UserId == userId,
                cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro ao buscar os dados do treino.", ex);
        }
    }

    public async Task SaveAsync(WorkoutSession workoutSession, CancellationToken cancellationToken)
    {
        try
        {
            await context.WorkoutSessions.AddAsync(workoutSession, cancellationToken);
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