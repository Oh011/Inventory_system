using Domain.Common;

namespace Domain.Entities
{
    public class UserNotification : BaseEntity
    {
        public string UserId { get; set; }
        public bool IsSeen { get; set; } = false;

        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
    }
}
