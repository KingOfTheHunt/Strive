using Flunt.Notifications;

namespace Strive.Application.Users.UseCases.Create;

public class Response : 
    Core.Abstractions.Response
{
    public Response(bool success, string message, int statusCode, 
        IReadOnlyCollection<Notification>? notifications = null) : base(success, message, statusCode, notifications)
    {
    }
}