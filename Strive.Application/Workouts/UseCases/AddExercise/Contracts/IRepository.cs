using Strive.Core.Entities;

namespace Strive.Application.Workouts.UseCases.AddExercise.Contracts;

public interface IRepository
{
    Task<Workout?> GetWorkoutByIdAsync(int id, int userId, CancellationToken cancellationToken);
    Task<bool> AnyExerciseAsync(int exerciseId, CancellationToken cancellationToken);
    Task SaveAsync(Workout workout, CancellationToken cancellationToken);
}