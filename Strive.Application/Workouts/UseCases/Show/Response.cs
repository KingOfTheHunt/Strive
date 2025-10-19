using Flunt.Notifications;

namespace Strive.Application.Workouts.UseCases.Show;

public class Response(bool success, string message, int statusCode, 
    IReadOnlyCollection<Notification>? notifications = null) 
    : Core.Abstractions.Response(success, message, statusCode, notifications)
{
    public IReadOnlyCollection<ResponseData> Data { get; set; }

    public Response(bool sucess, string message, int statusCode, IReadOnlyCollection<ResponseData> data)
    : this(sucess, message, statusCode)
    {
        Data = data;
    }
}

public class ResponseData
{
    public int WorkoutId { get; set; }
    public string WorkoutName { get; set; } = string.Empty;
}