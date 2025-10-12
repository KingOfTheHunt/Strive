using MedTheMediator.Abstractions;

namespace Strive.Application.Users.UseCases.ResetPassword;

public record Request(string Email, string NewPassword, string NewPasswordAgain, string ResetCode) : IRequest<Response>;