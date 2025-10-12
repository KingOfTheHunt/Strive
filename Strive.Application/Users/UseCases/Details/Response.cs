using Flunt.Notifications;

namespace Strive.Application.Users.UseCases.Details;

public class Response(bool success, string message, int statusCode, 
    IReadOnlyCollection<Notification>? notifications = null) : 
    Core.Abstractions.Response(success, message, statusCode, notifications)
{
    public ResponseData? Data { get; set; }

    public Response(bool success, string message, int statusCode, ResponseData data)
        : this(success, message, statusCode)
    {
        Data = data;
    }
}

public class ResponseData(string name, string email)
{
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
} 