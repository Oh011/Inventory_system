using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.Repositories;

namespace Project.Application.Features.Notifications.Queries.GetUnseenCount
{
    internal class GetUnseenNotificationsCountQueryHandler : IRequestHandler<GetUnseenNotificationsCountQuery, NotificationsCount>
    {


        private readonly IUnitOfWork _unitOfWork;


        public GetUnseenNotificationsCountQueryHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<NotificationsCount> Handle(GetUnseenNotificationsCountQuery request, CancellationToken cancellationToken)
        {

            var repository = _unitOfWork.GetRepository<Notification, int>();


            var count = await repository.CountAsync(n => n.UserNotifications.Any
            (un => un.UserId == request.userId && un.IsSeen == false));


            return new NotificationsCount(count);

        }
    }
}
