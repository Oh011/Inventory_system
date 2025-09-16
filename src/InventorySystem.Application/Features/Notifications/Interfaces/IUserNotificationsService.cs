using InventorySystem.Application.Features.Notifications.Dtos;
using InventorySystem.Application.Features.Notifications.Queries.GetUserNotifications;
using Shared.Results;

namespace InventorySystem.Application.Features.Notifications.Interfaces
{
    public interface IUserNotificationsService
    {


        Task<UserNotificationDto> CreateNotification(CreateUserNotificationDto dto);


        Task<IEnumerable<UserNotificationDto>> CreateNotifications(List<CreateUserNotificationDto> notifications);

        Task<PaginatedResult<UserNotificationDto>> GetUserNotifications(GetUserNotificationsQuery query);
    }
}
