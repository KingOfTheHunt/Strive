using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Strive.Application.UseCases.Users.Create.Contracts;
using Strive.Core;
using Strive.Infrastructure.Data;
using Strive.Infrastructure.UseCases.Users.Create;

namespace Strive.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<Strive.Application.UseCases.Users.Create.Contracts.IRepository,
            Strive.Infrastructure.UseCases.Users.Create.Repository>();

        services.AddTransient<Application.UseCases.Users.Verify.Contracts.IRepository,
            Infrastructure.UseCases.Users.Verify.Repository>();
        services.AddTransient<Application.UseCases.Users.Verify.Contracts.IEmailService,
            Infrastructure.UseCases.Users.Verify.EmailService>();
        return services;
    }
}