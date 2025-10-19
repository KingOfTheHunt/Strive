using MedTheMediator.Abstractions;

namespace Strive.Application.Workouts.UseCases.Details;

public record Request(int WorkoutId, int UserId) : IRequest<Response>;