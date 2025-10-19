using Microsoft.EntityFrameworkCore;
using Strive.Application.Workouts.UseCases.Show.Contracts;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Workouts.UseCases.Show;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<IEnumerable<string>> GetAllWorkoutsAsync(int userId, CancellationToken cancellationToken)
    {
        return await context.Workouts.AsNoTracking().Where(x => x.UserId == userId)
            .Select(x => x.Name).ToArrayAsync(cancellationToken);
    }
}