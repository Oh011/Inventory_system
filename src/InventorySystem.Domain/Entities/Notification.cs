using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public NotificationType Type { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }
    }

}
