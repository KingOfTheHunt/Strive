using Flunt.Notifications;

namespace Strive.Application.Workouts.UseCases.Show;

public class Response(bool success, string message, int statusCode, 
    IReadOnlyCollection<Notification>? notifications = null) 
    : Core.Abstractions.Response(success, message, statusCode, notifications)
{
    public ResponseData? Data { get; set; }

    public Response(bool sucess, string message, int statusCode, ResponseData data)
    : this(sucess, message, statusCode)
    {
        Data = data;
    }
}

public class ResponseData
{
    public IEnumerable<string> Workouts { get; set; }

    public ResponseData(IEnumerable<string> workouts)
    {
        Workouts = workouts;
    }
}