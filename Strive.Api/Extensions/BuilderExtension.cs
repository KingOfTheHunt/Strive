using Microsoft.EntityFrameworkCore;
using Strive.Core;
using Strive.Infrastructure.Data;

namespace Strive.Api.Extensions;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.Database.Connection = builder.Configuration
            .GetConnectionString("Default") ?? string.Empty;
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(Configuration.Database.Connection, x =>
                x.MigrationsAssembly(typeof(Program).Assembly));
        });
    }
}