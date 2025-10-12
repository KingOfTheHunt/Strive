using MedTheMediator.Abstractions;

namespace Strive.Application.Users.UseCases.Details;

public record Request(int Id) : IRequest<Response>;