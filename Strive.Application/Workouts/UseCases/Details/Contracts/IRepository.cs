namespace Strive.Application.Workouts.UseCases.Details.Contracts;

public interface IRepository
{
    Task<bool> AnyWorkoutAsync(int workoutId, int userId, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<ResponseData>> GetExercisesAsync(int workoutId, int userId, CancellationToken cancellationToken);
}