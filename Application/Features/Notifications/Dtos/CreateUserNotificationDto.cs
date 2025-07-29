
using Domain.Enums;

namespace Project.Application.Features.Notifications.Dtos
{
    public class CreateUserNotificationDto
    {

        public string Message { get; set; }
        public NotificationType NotificationType { get; set; }

        public IEnumerable<string> Users { get; set; }
    }
}
