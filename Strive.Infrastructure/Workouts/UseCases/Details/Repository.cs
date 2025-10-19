using Microsoft.EntityFrameworkCore;
using Strive.Application.Workouts.UseCases.Details;
using Strive.Application.Workouts.UseCases.Details.Contracts;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Workouts.UseCases.Details;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<bool> AnyWorkoutAsync(int workoutId, int userId, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Workouts.AnyAsync(x => x.Id == workoutId && x.UserId == userId,
                cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro inesperado com o banco de dados.", ex);
        }
    }

    public async Task<IReadOnlyCollection<ResponseData>> GetExercisesAsync(int workoutId, int userId, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Workouts
                .Where(x => x.Id == workoutId && x.UserId == userId)
                .SelectMany(x => x.WorkoutExercises)
                .Select(x => new ResponseData()
                {
                    ExerciseName = x.Exercise.Name,
                    Sets = x.Sets.Sets,
                    Repetitions = x.Repetitions.Repetitions,
                    Weight = x.Weight.Weight,
                    Duration = x.Duration.TimeInSeconds
                })
                .ToArrayAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Houve um erro inesperado.", ex);
        }
    }
}