using MedTheMediator.Abstractions;

namespace Strive.Application.Users.UseCases.SendPasswordResetCode;

public record Request(string Email) : IRequest<Response>;