namespace Project.Application.Features.Notifications.Dtos
{
    public class UserNotificationDto
    {

        public int NotificationId { get; set; }            // ID to identify and mark as seen
        public string Message { get; set; }


        public string NotificationType { get; set; } = string.Empty;
        public string? Link { get; set; }                  // Optional: Link to redirect (e.g., /orders/15)
        public bool IsSeen { get; set; }
    }
}
