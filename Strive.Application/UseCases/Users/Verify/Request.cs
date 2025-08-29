using MedTheMediator.Abstractions;

namespace Strive.Application.UseCases.Users.Verify;

public record Request(string Email, string VerificationCode) : IRequest<Response>;