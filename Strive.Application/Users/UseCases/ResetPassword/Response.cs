using Flunt.Notifications;

namespace Strive.Application.Users.UseCases.ResetPassword;

public class Response(bool success, string message, int statusCode, IReadOnlyCollection<Notification>? notifications = null) 
    : Core.Abstractions.Response(success, message, statusCode, notifications);