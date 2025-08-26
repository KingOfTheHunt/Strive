using Flunt.Notifications;

namespace Strive.Core.Abstractions;

public abstract record Response
{
    public bool IsSuccess { get; protected set; }
    public int StatusCode { get; protected set; }
    public string Message { get; protected set; }
    public IEnumerable<Notification>? Notifications { get; protected set; }

    protected Response(bool isSuccess, string message, int statusCode, IEnumerable<Notification>? notifications = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
        Notifications = notifications;
    }
}