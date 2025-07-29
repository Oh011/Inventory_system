using MediatR;

namespace Project.Application.Features.Notifications.Queries.GetUnseenCount
{
    public class GetUnseenNotificationsCountQuery : IRequest<NotificationsCount>
    {

        public string userId { get; set; }





        public GetUnseenNotificationsCountQuery(string userId)
        {
            this.userId = userId;

        }
    }


    public record NotificationsCount(int UnseenCount);
}
