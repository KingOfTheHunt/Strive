using Scalar.AspNetCore;
using Strive.Api.Extensions;
using Strive.Application;
using Strive.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDatabase();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.MapUserEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("docs", options =>
    {
        options.Title = "Strive Api";
    });
}

app.UseHttpsRedirection();
app.Run();