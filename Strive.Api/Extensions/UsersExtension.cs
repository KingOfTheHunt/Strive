using MedTheMediator.Abstractions;

namespace Strive.Api.Extensions;

public static class UsersExtension
{
    public static void MapUsersEndpoints(this WebApplication app)
    {
        // Create
        app.MapPost("api/v1/users/create", async (Application.UseCases.Users.Create.Request request,
                IMediator mediator) =>
            {
                var result = await mediator.SendAsync<Application.UseCases.Users.Create.Request,
                    Application.UseCases.Users.Create.Response>(request);

                if (result.IsSuccess)
                    return Results.Created("", result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .WithTags("Users")
            .WithDisplayName("Create")
            .WithDescription("Create an account")
            .Produces<Application.UseCases.Users.Create.Response>(statusCode: 201)
            .Produces<Application.UseCases.Users.Create.Response>(statusCode: 400);

        // Verify account
        app.MapPut("api/v1/users/verify", async (Application.UseCases.Users.Verify.Request request,
                IMediator mediator) =>
            {
                var result = await mediator.SendAsync<Application.UseCases.Users.Verify.Request,
                    Application.UseCases.Users.Verify.Response>(request);

                if (result.IsSuccess)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .WithTags("Users")
            .WithDisplayName("Verify")
            .WithDescription("Verify an account.")
            .Produces<Application.UseCases.Users.Verify.Response>(statusCode: 200)
            .Produces<Application.UseCases.Users.Verify.Response>(statusCode: 400)
            .Produces<Application.UseCases.Users.Verify.Response>(statusCode: 404);

        // Resend verification code
        app.MapPut("api/v1/users/resend-verification", async (
                Application.UseCases.Users.ResendVerification.Request request, IMediator mediator) =>
            {
                var result = await mediator.SendAsync<Application.UseCases.Users.ResendVerification.Request,
                    Application.UseCases.Users.ResendVerification.Response>(request);

                if (result.IsSuccess)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .WithTags("Users")
            .WithDisplayName("Resend verification")
            .WithDescription("Resend the verification code to user e-mail.")
            .Produces<Application.UseCases.Users.ResendVerification.Response>()
            .Produces<Application.UseCases.Users.ResendVerification.Response>(statusCode: 400)
            .Produces<Application.UseCases.Users.ResendVerification.Response>(statusCode: 404);

        // Authenticate
        app.MapPost("api/v1/users/auth", async (Application.UseCases.Users.Authenticate.Request request,
                IMediator mediator) =>
            {
                var res = await mediator.SendAsync<Application.UseCases.Users.Authenticate.Request,
                    Application.UseCases.Users.Authenticate.Response>(request);

                if (res is { IsSuccess: true, Data: not null })
                {
                    var result = res with { Data = res.Data with { Token = JwtExtension.Generate(res.Data) } };
                    
                    return Results.Ok(result);
                }

                return Results.Json(res, statusCode: res.StatusCode);
            })
            .WithTags("Users")
            .WithDisplayName("Authenticate")
            .WithDescription("Authenticate an account and generate a JWT")
            .Produces<Application.UseCases.Users.Authenticate.Response>()
            .Produces<Application.UseCases.Users.Authenticate.Response>(statusCode: 400)
            .Produces<Application.UseCases.Users.Authenticate.Response>(statusCode: 404);
        
        // Details
        app.MapGet("api/v1/users/details", async (HttpContext context, IMediator mediator) =>
            {
                var userId = int.Parse(context.User.Identity.Name);
                var request = new Application.UseCases.Users.Details.Request(userId);
                var result = await mediator.SendAsync<Application.UseCases.Users.Details.Request,
                    Application.UseCases.Users.Details.Response>(request);

                if (result.IsSuccess)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .WithTags("Users")
            .WithDescription("Get the account data")
            .Produces<Application.UseCases.Users.Details.Response>()
            .Produces<Application.UseCases.Users.Details.Response>(statusCode: 400)
            .Produces(statusCode: 401)
            .Produces<Application.UseCases.Users.Details.Response>(statusCode: 404)
            .RequireAuthorization();
    }
}