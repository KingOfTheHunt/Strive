using MedTheMediator.Abstractions;

namespace Strive.Application.Users.UseCases.ResendVerification;

public record Request(string Email) : IRequest<Response>;