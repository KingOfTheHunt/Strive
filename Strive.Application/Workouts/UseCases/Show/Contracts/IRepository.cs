namespace Strive.Application.Workouts.UseCases.Show.Contracts;

public interface IRepository
{
    Task<IEnumerable<string>> GetAllWorkoutsAsync(int userId, CancellationToken cancellationToken);
}