using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
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

        Configuration.JwtKey = builder.Configuration.GetValue<string>("JwtKey")
                               ?? string.Empty;
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(Configuration.Database.Connection, x =>
                x.MigrationsAssembly(typeof(Program).Assembly));
        });
    }

    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
    }

    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.JwtKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        builder.Services.AddAuthorization();
    }
}