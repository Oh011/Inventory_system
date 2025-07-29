using Project.Application.Features.Notifications.Dtos;
using Project.Application.Features.Notifications.Queries.GetUserNotifications;
using Shared.Results;

namespace Project.Application.Features.Notifications.Interfaces
{
    public interface IUserNotificationsService
    {


        Task<UserNotificationDto> CreateNotification(CreateUserNotificationDto dto);


        Task<IEnumerable<UserNotificationDto>> CreateNotifications(List<CreateUserNotificationDto> notifications);

        Task<PaginatedResult<UserNotificationDto>> GetUserNotifications(GetUserNotificationsQuery query);
    }
}
