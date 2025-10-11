using Flunt.Notifications;

namespace Strive.Application.Users.UseCases.Authenticate;

public class Response(bool success, string message, int statusCode, 
    IReadOnlyCollection<Notification>? notifications = null) 
    : Core.Abstractions.Response(success, message, statusCode, notifications)
{
    public ResponseData? Data { get; set; }

    public Response(bool success, string message, int statusCode, ResponseData data) 
        : this(success, message, statusCode)
    {
        Data = data;
    }
}

public class ResponseData
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}