using System.Text.Json.Serialization;
using MedTheMediator.Abstractions;

namespace Strive.Application.Sessions.UseCases.Reschedule;

public record Request(
    [property: JsonIgnore] int WorkoutSessionId,
    [property: JsonIgnore] int UserId,
    DateTime ScheduleDate) : IRequest<Response>;