using System.Text.Json.Serialization;
using MedTheMediator.Abstractions;

namespace Strive.Application.Workouts.UseCases.RemoveExercise;

public record Request([property: JsonIgnore] int UserId, [property: JsonIgnore] int WorkoutId, 
    [property: JsonIgnore] int ExerciseId)
: IRequest<Response>;