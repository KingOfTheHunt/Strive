using Strive.Api.Extensions;
using Strive.Application;
using Strive.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.Services.AddDatabase();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();
app.MapUsersEndpoints();

app.MapGet("/", () => "Hello World!");

app.Run();