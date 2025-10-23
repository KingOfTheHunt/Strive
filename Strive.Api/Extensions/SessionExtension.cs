using MedTheMediator.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Strive.Api.Extensions;

public static class SessionExtension
{
    public static void MapSessionEndpoints(this WebApplication app)
    {
        // Sessions - Schedule
        app.MapPost("v1/sessions/schedule/{workoutId}", async (HttpContext context,
                int workoutId, Application.Sessions.UseCases.Schedule.Request request, IMediator mediator) =>
            {
                int.TryParse(context.User.Identity!.Name, out int userId);

                var result = await mediator.SendAsync<Application.Sessions.UseCases.Schedule.Request,
                    Application.Sessions.UseCases.Schedule.Response>(
                    request with { WorkoutId = workoutId, UserId = userId });

                if (result.Success)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .RequireAuthorization()
            .WithTags("Sessions")
            .WithDescription("Schedule a workout")
            .Produces<Application.Sessions.UseCases.Schedule.Response>()
            .Produces<Application.Sessions.UseCases.Schedule.Response>(400)
            .Produces<Application.Sessions.UseCases.Schedule.Response>(404)
            .Produces<Application.Sessions.UseCases.Schedule.Response>(500);

        // Sessions - Reschedule
        app.MapPost("v1/sessions/reschedule/{workoutSessionId}", async (HttpContext context,
                int workoutSessionId, Application.Sessions.UseCases.Reschedule.Request request, IMediator mediator) =>
            {
                int.TryParse(context.User.Identity!.Name, out int userId);

                var result = await mediator.SendAsync<Application.Sessions.UseCases.Reschedule.Request,
                    Application.Sessions.UseCases.Reschedule.Response>(request with
                {
                    WorkoutSessionId = workoutSessionId,
                    UserId = userId
                });

                if (result.Success)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .RequireAuthorization()
            .WithTags("Sessions")
            .WithDescription("Reschedule workout")
            .Produces<Application.Sessions.UseCases.Reschedule.Response>()
            .Produces<Application.Sessions.UseCases.Reschedule.Response>(400)
            .Produces<Application.Sessions.UseCases.Reschedule.Response>(404)
            .Produces<Application.Sessions.UseCases.Reschedule.Response>(500);
        
        // Sessions - List
        app.MapGet("v1/sessions/list", async (HttpContext context,
                [FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
                IMediator mediator) =>
            {
                int.TryParse(context.User.Identity!.Name, out int userId);
                var request = new Application.Sessions.UseCases.List.Request(userId, startDate, endDate);

                var result = await mediator.SendAsync<Application.Sessions.UseCases.List.Request,
                    Application.Sessions.UseCases.List.Response>(request);

                if (result.Success)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .RequireAuthorization()
            .WithTags("Sessions")
            .WithDescription("Show the workouts that has been scheduled.")
            .Produces<Application.Sessions.UseCases.List.Response>()
            .Produces<Application.Sessions.UseCases.List.Response>(400)
            .Produces<Application.Sessions.UseCases.List.Response>(500);
    }
}