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
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(Configuration.Database.Connection,
                x => x.MigrationsAssembly("Strive.Api"));
        });
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<Strive.Application.UseCases.Users.Create.Contracts.IRepository,
            Strive.Infrastructure.UseCases.Users.Create.Repository>();
        return services;
    }
}