using Microsoft.Extensions.DependencyInjection;

namespace Strive.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Create
        services.AddTransient<Strive.Application.UseCases.Users.Create.Contracts.IEmailService,
            Strive.Infrastructure.UseCases.Users.Create.EmailService>();
        services.AddTransient<Strive.Application.UseCases.Users.Create.Contracts.IRepository,
            Strive.Infrastructure.UseCases.Users.Create.Repository>();

        // Verify
        services.AddTransient<Application.UseCases.Users.Verify.Contracts.IRepository,
            Infrastructure.UseCases.Users.Verify.Repository>();
        services.AddTransient<Application.UseCases.Users.Verify.Contracts.IEmailService,
            Infrastructure.UseCases.Users.Verify.EmailService>();

        // Resend verification
        services.AddTransient<Application.UseCases.Users.ResendVerification.Contracts.IRepository,
            UseCases.Users.ResendVerification.Repository>();
        services.AddTransient<Application.UseCases.Users.ResendVerification.Contracts.IEmailService,
            UseCases.Users.ResendVerification.EmailService>();

        return services;
    }
}