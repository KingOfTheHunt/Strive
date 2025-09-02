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
        });

        // Verify account
        app.MapPut("api/v1/users/verify", async (Application.UseCases.Users.Verify.Request request,
            IMediator mediator) =>
        {
            var result = await mediator.SendAsync<Application.UseCases.Users.Verify.Request,
                Application.UseCases.Users.Verify.Response>(request);

            if (result.IsSuccess)
                return Results.Ok(result);

            return Results.Json(result, statusCode: result.StatusCode);
        });
        
        // Resend verification code
        app.MapPut("api/v1/users/resend-verification", async (
            Application.UseCases.Users.ResendVerification.Request request, IMediator mediator) =>
        {
            var result = await mediator.SendAsync<Application.UseCases.Users.ResendVerification.Request,
                Application.UseCases.Users.ResendVerification.Response>(request);

            if (result.IsSuccess)
                return Results.Ok(result);

            return Results.Json(result, statusCode: result.StatusCode);
        });
    }
}