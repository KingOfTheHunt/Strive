using Flunt.Notifications;

namespace Strive.Application.UseCases.Users.Create;

public record Response(bool IsSuccess, string Message, int StatusCode, IEnumerable<Notification>? Notifications = null)
    : Core.Abstractions.Response(IsSuccess, Message, StatusCode, Notifications);