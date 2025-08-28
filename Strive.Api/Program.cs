using Scalar.AspNetCore;
using Strive.Api.Extensions;
using Strive.Application;
using Strive.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.Services.AddDatabase();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddOpenApi();

var app = builder.Build();
app.MapUsersEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("docs");
}

app.MapGet("/", () => "Hello World!");

app.Run();