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
builder.AddJwtAuth();

var app = builder.Build();
app.MapUsersEndpoints();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("docs");
}

app.MapGet("/", () => "Hello World!");

app.Run();