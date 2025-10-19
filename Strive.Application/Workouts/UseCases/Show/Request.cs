using System.Text.Json.Serialization;
using MedTheMediator.Abstractions;

namespace Strive.Application.Workouts.UseCases.Show;

public record Request([property: JsonIgnore] int UserId) : IRequest<Response>;