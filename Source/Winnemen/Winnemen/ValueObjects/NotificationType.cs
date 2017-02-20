namespace Winnemen.ValueObjects
{
    public class NotificationType : ValueObject<NotificationType>
    {
        public static readonly NotificationType Success = new NotificationType {Id = 1, Name = "Success"};
        public static readonly NotificationType Error = new NotificationType { Id = 2, Name = "Error" };
        public static readonly NotificationType Info = new NotificationType { Id = 3, Name = "Info" };
        public static readonly NotificationType Warning = new NotificationType { Id = 4, Name = "Warning" };
    }
}
