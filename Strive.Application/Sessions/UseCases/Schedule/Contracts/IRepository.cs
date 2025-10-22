using Strive.Core.Entities;

namespace Strive.Application.Sessions.UseCases.Schedule.Contracts;

public interface IRepository
{
    Task<bool> AnyWorkoutAsync(int workoutId, int userId, CancellationToken cancellationToken);
    Task SaveAsync(WorkoutSession workoutSession, CancellationToken cancellationToken);
}