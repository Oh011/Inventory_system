using Domain.Entities;
using Domain.Specifications;
using Project.Application.Features.Notifications.Dtos;
using Project.Application.Features.Notifications.Queries.GetUserNotifications;

namespace Project.Application.Features.Notifications.Specifications
{
    public class UserNotificationsSpecification : ProjectionSpecifications<Notification, UserNotificationDto>
    {




        public UserNotificationsSpecification(GetUserNotificationsQuery query) : base(

            n => (n.UserNotifications.Any(un => un.UserId == query.userId)) &&

            (!query.IsSeen.HasValue || n.UserNotifications.Any(un => un.IsSeen == query.IsSeen))

            )

        {


            AddProjection(n => new UserNotificationDto
            {

                NotificationId = n.Id,
                NotificationType = n.Type.ToString(),
                Message = n.Message,
                IsSeen = n.UserNotifications.Where(un => un.UserId == query.userId && un.NotificationId == n.Id).Select(un => un.IsSeen).FirstOrDefault(),

            });


            SetOrderByDescending(n => n.CreatedAt);


            ApplyPagination(query.PageIndex, query.pageSize);

        }
    }
}
