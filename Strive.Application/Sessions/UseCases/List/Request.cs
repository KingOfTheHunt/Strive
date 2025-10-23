using System.Text.Json.Serialization;
using MedTheMediator.Abstractions;

namespace Strive.Application.Sessions.UseCases.List;

public record Request([property: JsonIgnore] int UserId, DateTime StartDate, DateTime EndDate) : IRequest<Response>;