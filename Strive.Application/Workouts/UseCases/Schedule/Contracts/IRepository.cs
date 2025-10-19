using Strive.Core.Entities;

namespace Strive.Application.Workouts.UseCases.Schedule.Contracts;

public interface IRepository
{
    Task<Workout?> GetWorkoutByIdAsync(int workoutId, int userId, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}