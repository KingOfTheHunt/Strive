using MedTheMediator.Abstractions;

namespace Strive.Api.Extensions;

public static class UserExtension
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        // Users - Create
        app.MapPost("v1/users/create", async (Application.Users.UseCases.Create.Request request,
                IMediator mediator) =>
            {
                var result = await mediator.SendAsync<Application.Users.UseCases.Create.Request,
                    Application.Users.UseCases.Create.Response>(request);

                if (result.Success)
                    return Results.Created("v1/users/me", result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .WithTags("Users")
            .WithDescription("Create an account")
            .Produces<Application.Users.UseCases.Create.Response>(201)
            .Produces<Application.Users.UseCases.Create.Response>(400)
            .Produces<Application.Users.UseCases.Create.Response>(500);

        app.MapPost("v1/users/verify", async (Application.Users.UseCases.Verify.Request request,
                IMediator mediator) =>
            {
                var result = await mediator.SendAsync<Application.Users.UseCases.Verify.Request,
                    Application.Users.UseCases.Verify.Response>(request);

                if (result.Success)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .WithTags("Users")
            .WithDescription("Verify an user account")
            .Produces<Application.Users.UseCases.Verify.Response>()
            .Produces<Application.Users.UseCases.Verify.Response>(400)
            .Produces<Application.Users.UseCases.Verify.Response>(404)
            .Produces<Application.Users.UseCases.Verify.Response>(500);

    }
}