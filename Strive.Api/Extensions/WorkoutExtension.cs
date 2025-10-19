using MedTheMediator.Abstractions;

namespace Strive.Api.Extensions;

public static class WorkoutExtension
{
    public static void MapWorkoutEndpoints(this WebApplication app)
    {
        // Workouts - Create
        app.MapPost("v1/workouts/create", async (HttpContext context,
                Application.Workouts.UseCases.Create.Request request, IMediator mediator) =>
            {
                int.TryParse(context.User.Identity!.Name, out int userId);

                var result = await mediator.SendAsync<Application.Workouts.UseCases.Create.Request,
                    Application.Workouts.UseCases.Create.Response>(request with { UserId = userId });

                if (result.Success)
                    return Results.Created($"v1/workouts/{result.Data!.WorkoutId}", result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .RequireAuthorization()
            .WithTags("Workouts")
            .WithDescription("Create a new workout")
            .Produces<Application.Workouts.UseCases.Create.Response>(201)
            .Produces<Application.Workouts.UseCases.Create.Response>(400)
            .Produces<Application.Workouts.UseCases.Create.Response>(500);

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

        // Workouts - Remove Exercise
        app.MapDelete("v1/workouts/{workoutId:int}/remove-exercise/{exerciseId:int}", async (HttpContext context,
                int workoutId, int exerciseId, IMediator mediator) =>
            {
                int.TryParse(context.User.Identity!.Name, out int userId);
                var request = new Application.Workouts.UseCases.RemoveExercise.Request(userId, workoutId, exerciseId);
                var result = await mediator.SendAsync<Application.Workouts.UseCases.RemoveExercise.Request,
                    Application.Workouts.UseCases.RemoveExercise.Response>(request);

                if (result.Success)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .RequireAuthorization()
            .WithTags("Workouts")
            .WithDescription("Remove an exercise from a workout.")
            .Produces<Application.Workouts.UseCases.RemoveExercise.Response>()
            .Produces<Application.Workouts.UseCases.RemoveExercise.Response>(400)
            .Produces<Application.Workouts.UseCases.RemoveExercise.Response>(404)
            .Produces<Application.Workouts.UseCases.RemoveExercise.Response>(500);

        // Workouts - Update Exercise
        app.MapPut("v1/workouts/{workoutId:int}/update-exercise/{exerciseId}", async (HttpContext context,
                int workoutId, int exerciseId, Application.Workouts.UseCases.UpdateExercise.Request request,
                IMediator mediator) =>
            {
                int.TryParse(context.User.Identity!.Name, out int userId);
                var result = await mediator.SendAsync<Application.Workouts.UseCases.UpdateExercise.Request,
                    Application.Workouts.UseCases.UpdateExercise.Response>(request with
                {
                    WorkoutId = workoutId, UserId = userId, ExerciseId = exerciseId
                });

                if (result.Success)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .RequireAuthorization()
            .WithTags("Workouts")
            .WithDescription("Update an exercise")
            .Produces<Application.Workouts.UseCases.UpdateExercise.Response>()
            .Produces<Application.Workouts.UseCases.UpdateExercise.Response>(400)
            .Produces<Application.Workouts.UseCases.UpdateExercise.Response>(404)
            .Produces<Application.Workouts.UseCases.UpdateExercise.Response>(500);

        app.MapGet("v1/workouts/show", async (HttpContext context, IMediator mediator) =>
            {
                int.TryParse(context.User.Identity!.Name, out int userId);
                var request = new Application.Workouts.UseCases.Show.Request(userId);

                var result = await mediator.SendAsync<Application.Workouts.UseCases.Show.Request,
                    Application.Workouts.UseCases.Show.Response>(request);

                if (result.Success)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .RequireAuthorization()
            .WithTags("Workouts")
            .WithDescription("Show all workouts")
            .Produces<Application.Workouts.UseCases.Show.Response>()
            .Produces<Application.Workouts.UseCases.Show.Response>(400)
            .Produces<Application.Workouts.UseCases.Show.Response>(404)
            .Produces<Application.Workouts.UseCases.Show.Response>(500);
    }
}