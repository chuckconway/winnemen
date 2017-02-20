using System.Collections.Generic;

namespace Winnemen.Notifications
{
    public interface INotifications
    {
        IList<INotification> Notifications { get; set; }
    }
}
