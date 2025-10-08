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

        Configuration.Smtp.Host = builder.Configuration.GetSection("Smtp")
            .GetValue<string>("Host") ?? string.Empty;
        Configuration.Smtp.Port = builder.Configuration.GetSection("Smtp")
            .GetValue<int>("Port");
        Configuration.Smtp.Login = builder.Configuration.GetSection("Smtp")
            .GetValue<string>("Login") ?? string.Empty;
        Configuration.Smtp.Password = builder.Configuration.GetSection("Smtp")
            .GetValue<string>("Password") ?? string.Empty;
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