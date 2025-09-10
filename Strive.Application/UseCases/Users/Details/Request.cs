using MedTheMediator.Abstractions;

namespace Strive.Application.UseCases.Users.Details;

public record Request(int Id) : IRequest<Response>;