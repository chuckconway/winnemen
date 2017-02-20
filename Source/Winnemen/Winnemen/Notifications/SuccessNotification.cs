using Winnemen.ValueObjects;

namespace Winnemen.Notifications
{
    public class SuccessNotification : INotification
    {
        public SuccessNotification()
        {
            
        }

        public SuccessNotification(string message)
        {
            Message = message;
        }

        public NotificationType NotificationType { get; } = NotificationType.Info;
        public string Message { get; set; }
    }
}