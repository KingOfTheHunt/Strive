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

        // Verify
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

        // Authenticate
        app.MapPost("v1/users/auth", async (Application.Users.UseCases.Authenticate.Request request,
                IMediator mediator) =>
            {
                var result = await mediator.SendAsync<Application.Users.UseCases.Authenticate.Request,
                    Application.Users.UseCases.Authenticate.Response>(request);

                if (result.Success)
                {
                    result.Data!.Token = JwtExtension.Generate(result.Data);
                    return Results.Ok(result);
                }

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .WithTags("Users")
            .WithDescription("Generates a JWT")
            .Produces<Application.Users.UseCases.Authenticate.Response>()
            .Produces<Application.Users.UseCases.Authenticate.Response>(400)
            .Produces<Application.Users.UseCases.Authenticate.Response>(500);

        // Resend Verification Code
        app.MapPost("v1/user/resend-verification", async (Application.Users.UseCases.ResendVerification.Request
                request, IMediator mediator) =>
            {
                var result = await mediator.SendAsync<Application.Users.UseCases.ResendVerification.Request,
                    Application.Users.UseCases.ResendVerification.Response>(request);

                if (result.Success)
                    return Results.Ok(result);

                return Results.Json(result, statusCode: result.StatusCode);
            })
            .WithTags("Users")
            .WithDescription("Resend a new verification code.")
            .Produces<Application.Users.UseCases.ResendVerification.Response>()
            .Produces<Application.Users.UseCases.ResendVerification.Response>(400)
            .Produces<Application.Users.UseCases.ResendVerification.Response>(404)
            .Produces<Application.Users.UseCases.ResendVerification.Response>(500);
    }
}