using MediatR;

namespace InventorySystem.Application.Features.Notifications.Commands.MarkNotificationAsSeen
{
    public class MarkNotificationAsSeenCommand : IRequest<string>
    {

        public int NotificationId { get; set; }


        public string UserId { get; set; }

        public MarkNotificationAsSeenCommand(int notificationId, string userId)
        {
            this.UserId = userId;
            this.NotificationId = notificationId;

        }
    }
}
