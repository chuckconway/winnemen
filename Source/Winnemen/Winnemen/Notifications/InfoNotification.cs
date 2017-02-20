using Winnemen.ValueObjects;

namespace Winnemen.Notifications
{
    public class InfoNotification : INotification
    {
        public InfoNotification()
        {
            
        }

        public InfoNotification(string message)
        {
            Message = message;
        }

        public NotificationType NotificationType { get; } = NotificationType.Info;
        public string Message { get; set; }
    }
}