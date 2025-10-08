using Flunt.Notifications;

namespace Strive.Core.Abstractions;

public abstract class Response(
    bool success,
    string message,
    int statusCode,
    IReadOnlyCollection<Notification>? notifications = null)
{
    public bool Success { get; set; }= success;
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public IReadOnlyCollection<Notification>? Notifications { get; set; } = notifications;
}