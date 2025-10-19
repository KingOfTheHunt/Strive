using Flunt.Notifications;

namespace Strive.Application.Workouts.UseCases.Schedule;

public class Response(bool success, string message, int statusCode, 
    IReadOnlyCollection<Notification>? notifications = null) 
    : Core.Abstractions.Response(success, message, statusCode, notifications);