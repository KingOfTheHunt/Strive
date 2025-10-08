using MedTheMediator.Abstractions;

namespace Strive.Application.Users.UseCases.Verify;

public record Request(string Email, string VerificationCode) : IRequest<Response>;