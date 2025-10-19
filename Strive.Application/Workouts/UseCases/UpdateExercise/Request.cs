using System.Text.Json.Serialization;
using MedTheMediator.Abstractions;

namespace Strive.Application.Workouts.UseCases.UpdateExercise;

public record Request(
    [property: JsonIgnore] int WorkoutId,
    [property: JsonIgnore] int UserId,
    [property: JsonIgnore] int ExerciseId,
    byte Sets,
    byte? Repetitions,
    byte? Weight,
    byte? Duration) : IRequest<Response>;