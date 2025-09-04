using MedTheMediator.Abstractions;

namespace Strive.Application.UseCases.Users.Authenticate;

public record Request(string Email, string Password) : IRequest<Response>;