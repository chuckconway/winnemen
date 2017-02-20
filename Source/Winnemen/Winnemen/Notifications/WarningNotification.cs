using Winnemen.ValueObjects;

namespace Winnemen.Notifications
{
    public class WarningNotification : INotification
    {
        public WarningNotification()
        {
            
        }

        public WarningNotification(string message)
        {
            Message = message;
        }

        public NotificationType NotificationType { get; } = NotificationType.Warning;
        public string Message { get; set; }
    }
}