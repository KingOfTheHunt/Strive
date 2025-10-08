using MedTheMediator.Abstractions;

namespace Strive.Application.Users.UseCases.Create;

public record Request(string FirstName, string LastName, string Email, string Password) : IRequest<Response>;