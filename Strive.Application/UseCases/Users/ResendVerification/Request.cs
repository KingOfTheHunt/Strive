using MedTheMediator.Abstractions;

namespace Strive.Application.UseCases.Users.ResendVerification;

public record Request(string Email) : IRequest<Response>;