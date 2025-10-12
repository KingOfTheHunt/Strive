using System.Text.Json.Serialization;
using MedTheMediator.Abstractions;

namespace Strive.Application.Users.UseCases.ChangePassword;

public record Request([property: JsonIgnore] int Id, string NewPassword, string NewPasswordAgain) : IRequest<Response>;