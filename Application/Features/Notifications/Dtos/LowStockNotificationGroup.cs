namespace Project.Application.Features.Notifications.Dtos
{
    public class LowStockNotificationGroup
    {
        public string Title { get; set; } = "Low Stock Products";
        public List<UserNotificationDto> Notifications { get; set; }
    }

}
