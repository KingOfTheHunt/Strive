using MedTheMediator.Abstractions;

namespace Strive.Application.Workouts.UseCases.Show;

public record Request(int UserId) : IRequest<Response>;