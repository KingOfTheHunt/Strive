using Flunt.Notifications;

namespace Strive.Application.Sessions.UseCases.List;

public class Response(
    bool success,
    string message,
    int statusCode,
    IReadOnlyCollection<Notification>? notifications = null)
    : Core.Abstractions.Response(success, message, statusCode, notifications)
{
    public IReadOnlyCollection<ResponseData>? Data { get; set; }

    public Response(bool success, string message, int statusCode, IReadOnlyCollection<ResponseData> data)
        : this(success, message, statusCode)
    {
        Data = data;
    }
}

public record ResponseData(string WorkoutName, DateTime ScheduleAt, bool Done);