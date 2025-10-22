using Strive.Core.Entities;

namespace Strive.Application.Sessions.UseCases.Reschedule.Contracts;

public interface IRepository
{
    Task<WorkoutSession?> GetWorkoutSessionByIdAsync(int workoutSessionId, int userId,
        CancellationToken cancellationToken);

    Task SaveAsync(WorkoutSession workoutSession, CancellationToken cancellationToken);
}