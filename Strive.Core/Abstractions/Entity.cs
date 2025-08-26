using Flunt.Notifications;

namespace Strive.Core.Abstractions;

public abstract class Entity : Notifiable<Notification>
{
    public int Id { get; set; }
}