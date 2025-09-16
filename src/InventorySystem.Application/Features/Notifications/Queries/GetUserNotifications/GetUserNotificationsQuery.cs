using MediatR;
using InventorySystem.Application.Features.Notifications.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace InventorySystem.Application.Features.Notifications.Queries.GetUserNotifications
{
    public class GetUserNotificationsQuery : PaginationQueryParameters, IRequest<PaginatedResult<UserNotificationDto>>
    {

        public string userId { get; set; }


        public bool? IsSeen { get; set; } = null;


        public GetUserNotificationsQuery(string userId, bool? IsSeen = null)
        {
            this.userId = userId;
            this.IsSeen = IsSeen;
        }
    }
}
