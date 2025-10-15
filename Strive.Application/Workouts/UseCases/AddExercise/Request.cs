using System.Text.Json.Serialization;
using MedTheMediator.Abstractions;

namespace Strive.Application.Workouts.UseCases.AddExercise;

public record Request([property: JsonIgnore] int WorkoutId, [property: JsonIgnore] int UserId, int ExerciseId, 
    byte Sets, byte? Repetitions, float? Weight, int? Duration) : IRequest<Response>;