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
    }
}