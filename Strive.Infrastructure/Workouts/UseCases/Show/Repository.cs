using Microsoft.EntityFrameworkCore;
using Strive.Application.Workouts.UseCases.Show;
using Strive.Application.Workouts.UseCases.Show.Contracts;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Workouts.UseCases.Show;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<IReadOnlyCollection<ResponseData>> GetAllWorkoutsAsync(int userId,
        CancellationToken cancellationToken)
    {
        try
        {
            return await context.Workouts.AsNoTracking().Where(x => x.UserId == userId)
                .Select(x => new ResponseData { WorkoutId = x.Id, WorkoutName = x.Name })
                .ToArrayAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro ao buscar os treino no banco de dados.", ex);
        }
    }
}