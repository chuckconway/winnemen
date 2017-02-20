using Winnemen.ValueObjects;

namespace Winnemen.Notifications
{
    public interface INotification
    {
        NotificationType NotificationType { get; }

        string Message { get; }
    }
}
