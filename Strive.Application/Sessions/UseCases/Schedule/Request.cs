using System.Text.Json.Serialization;
using MedTheMediator.Abstractions;

namespace Strive.Application.Sessions.UseCases.Schedule;

public record Request([property: JsonIgnore] int WorkoutId, [property: JsonIgnore] int UserId,
    DateTime ScheduleDate) : IRequest<Response>;