using System.Text.Json.Serialization;
using MedTheMediator.Abstractions;

namespace Strive.Application.Workouts.UseCases.Details;

public record Request([property: JsonIgnore] int WorkoutId, [property: JsonIgnore] int UserId) : IRequest<Response>;