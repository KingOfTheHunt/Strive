using Flunt.Notifications;

namespace Strive.Application.UseCases.Users.Details;

public record Response(bool IsSuccess, string Message, int StatusCode,
    IEnumerable<Notification>? Notifications = null)
    : Core.Abstractions.Response(IsSuccess, Message, StatusCode, Notifications)
{
    public ResponseData? Data { get; set; }

    public Response(bool isSuccess, string message, int statusCode, ResponseData data) 
        : this(isSuccess, message, statusCode)
    {
        Data = data;
    }
}

public record ResponseData(string Name, string Email);
