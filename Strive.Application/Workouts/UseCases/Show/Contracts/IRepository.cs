namespace Strive.Application.Workouts.UseCases.Show.Contracts;

public interface IRepository
{
    Task<IReadOnlyCollection<ResponseData>> GetAllWorkoutsAsync(int userId, CancellationToken cancellationToken);
}