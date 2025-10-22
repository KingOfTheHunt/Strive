using Microsoft.EntityFrameworkCore;
using Strive.Application.Sessions.UseCases.Reschedule.Contracts;
using Strive.Core.Entities;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Sessions.UseCases.Reschedule;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<WorkoutSession?> GetWorkoutSessionByIdAsync(int workoutSessionId, int userId, CancellationToken cancellationToken)
    {
        try
        {
            return await context.WorkoutSessions
                .FirstOrDefaultAsync(x => x.Id == workoutSessionId 
                                          && x.Workout.UserId == userId, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro ao buscar os dados no banco de dados.", ex);
        }
    }

    public async Task SaveAsync(WorkoutSession workoutSession, CancellationToken cancellationToken)
    {
        try
        {
            context.WorkoutSessions.Update(workoutSession);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Houve um erro na hora de salvar os dados no banco de dados.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro inesperado com o banco de dados.", ex);
        }
    }
}