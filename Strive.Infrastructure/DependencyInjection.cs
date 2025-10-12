using Microsoft.Extensions.DependencyInjection;

namespace Strive.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        // Users - Create
        services.AddTransient<Application.Users.UseCases.Create.Contracts.IRepository,
            Users.UseCases.Create.Repository>();
        services.AddTransient<Application.Users.UseCases.Create.Contracts.IEmailService,
            Users.UseCases.Create.EmailService>();
        
        // Users - Verify
        services.AddTransient<Application.Users.UseCases.Verify.Contracts.IRepository,
            Users.UseCases.Verify.Repository>();
        
        // Users - Authenticate
        services.AddTransient<Application.Users.UseCases.Authenticate.Contracts.IRepository,
            Users.UseCases.Authenticate.Repository>();
        
        // Users - Resend Verification
        services.AddTransient<Application.Users.UseCases.ResendVerification.Contracts.IRepository,
            Users.UseCases.ResendVerification.Repository>();
        services.AddTransient<Application.Users.UseCases.ResendVerification.Contracts.IEmailService,
            Users.UseCases.ResendVerification.EmailService>();
        
        // Users - Details
        services.AddTransient<Application.Users.UseCases.Details.Contracts.IRepository,
            Users.UseCases.Details.Repository>();
    }
}