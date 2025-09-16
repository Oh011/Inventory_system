using MediatR;

namespace InventorySystem.Application.Features.Notifications.Commands.MarkNotificationsAsSeen
{
    public class MarkAllNotificationsAsSeenCommand : IRequest<string>
    {

        public string userId { get; set; }





        public MarkAllNotificationsAsSeenCommand(string userId)
        {
            this.userId = userId;

        }
    }
}
