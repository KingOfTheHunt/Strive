using System.Text.Json.Serialization;
using MedTheMediator.Abstractions;

namespace Strive.Application.Users.UseCases.ChangeName;

public record Request([property: JsonIgnore] int Id,string FirstName, string LastName) : IRequest<Response>;