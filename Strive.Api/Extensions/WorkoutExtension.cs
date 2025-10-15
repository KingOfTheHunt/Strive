using MedTheMediator.Abstractions;

namespace Strive.Api.Extensions;

public static class WorkoutExtension
{
    public static void MapWorkoutEndpoints(this WebApplication app)
    {
        // Workouts - Add Exercise
        app.MapPost("v1/workouts/{id:int}/add-exercise", async (HttpContext context, int id,
                Application.Workouts.UseCases.AddExercise.Request request, IMediator mediator) =>
            {
                int.TryParse(context.User.Identity!.Name, out int userId);
                var result = await mediator.SendAsync<Application.Workouts.UseCases.AddExercise.Request,
                    Application.Workouts.UseCases.AddExercise.Response>(request
                    with
                    {
                        WorkoutId = id, UserId = userId
                    });

                if (result.Success)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .RequireAuthorization()
            .WithTags("Workouts")
            .WithDescription("Add an exercise to a workout")
            .Produces<Application.Workouts.UseCases.AddExercise.Response>()
            .Produces<Application.Workouts.UseCases.AddExercise.Response>(400)
            .Produces<Application.Workouts.UseCases.AddExercise.Response>(404)
            .Produces<Application.Workouts.UseCases.AddExercise.Response>(500);
    }
}