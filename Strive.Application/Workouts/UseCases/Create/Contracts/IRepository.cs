using Strive.Core.Entities;

namespace Strive.Application.Workouts.UseCases.Create.Contracts;

public interface IRepository
{
    Task<bool> AnyWorkoutAsync(string name, CancellationToken cancellationToken);
    Task SaveAsync(Workout workout, CancellationToken cancellationToken);
}