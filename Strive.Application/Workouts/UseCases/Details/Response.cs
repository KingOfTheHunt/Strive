using Flunt.Notifications;

namespace Strive.Application.Workouts.UseCases.Details;

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

public class ResponseData
{
    public string ExerciseName { get; set; } = string.Empty;
    public byte Sets { get; set; }
    public byte? Repetitions { get; set; }
    public float? Weight { get; set; }
    public int? Duration { get; set; }
}