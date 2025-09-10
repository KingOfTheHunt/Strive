using Microsoft.Extensions.DependencyInjection;

namespace Strive.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Create
        services.AddTransient<Strive.Application.UseCases.Users.Create.Contracts.IEmailService,
            UseCases.Users.Create.EmailService>();
        services.AddTransient<Strive.Application.UseCases.Users.Create.Contracts.IRepository,
            UseCases.Users.Create.Repository>();

        // Verify
        services.AddTransient<Application.UseCases.Users.Verify.Contracts.IRepository,
            UseCases.Users.Verify.Repository>();
        services.AddTransient<Application.UseCases.Users.Verify.Contracts.IEmailService,
            UseCases.Users.Verify.EmailService>();

        // Resend verification
        services.AddTransient<Application.UseCases.Users.ResendVerification.Contracts.IRepository,
            UseCases.Users.ResendVerification.Repository>();
        services.AddTransient<Application.UseCases.Users.ResendVerification.Contracts.IEmailService,
            UseCases.Users.ResendVerification.EmailService>();
        
        // Authenticate
        services.AddTransient<Application.UseCases.Users.Authenticate.Contracts.IRepository,
            UseCases.Users.Authenticate.Repository>();
        
        // Details
        services.AddTransient<Application.UseCases.Users.Details.Contracts.IRepository,
            UseCases.Users.Details.Repository>();

        return services;
    }
}