using MedTheMediator.Abstractions;

namespace Strive.Application.UseCases.Users.Create;

public record Request(string FirstName, string LastName, string Email, string Password) : IRequest<Response>;
