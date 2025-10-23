using Microsoft.EntityFrameworkCore;
using Strive.Application.Sessions.UseCases.List;
using Strive.Application.Sessions.UseCases.List.Contracts;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Sessions.UseCases.List;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<IReadOnlyCollection<ResponseData>> GetScheduleWorkoutsByDate(int userId, DateTime startDate,
        DateTime endDate, CancellationToken cancellationToken)
    {
        try
        {
            return await context.WorkoutSessions.AsNoTracking()
                .Where(x => x.Workout.UserId == userId &&
                            x.ScheduledAt.Date >= startDate && x.ScheduledAt.Date <= endDate)
                .Select(x => new ResponseData(x.Workout.Name,
                    x.ScheduledAt.Date!.Value, x.Done))
                .ToArrayAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro ao buscar os dados dos treino agendados.", ex);
        }
    }
}