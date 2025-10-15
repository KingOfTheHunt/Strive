using System.Text.Json.Serialization;
using MedTheMediator.Abstractions;

namespace Strive.Application.Workouts.UseCases.Create;

public record Request(string Name, [property: JsonIgnore] int UserId) : IRequest<Response>;