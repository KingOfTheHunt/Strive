using MedTheMediator.Abstractions;

namespace Strive.Application.Users.UseCases.Authenticate;

public record Request(string Email, string Password) : IRequest<Response>;