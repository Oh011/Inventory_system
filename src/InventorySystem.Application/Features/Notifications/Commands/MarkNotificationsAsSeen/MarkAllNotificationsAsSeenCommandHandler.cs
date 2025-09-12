using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.Repositories;

namespace Project.Application.Features.Notifications.Commands.MarkNotificationsAsSeen
{
    internal class MarkAllNotificationsAsSeenCommandHandler : IRequestHandler<MarkAllNotificationsAsSeenCommand, string>
    {

        private readonly IUnitOfWork _unitOfWork;


        public MarkAllNotificationsAsSeenCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(MarkAllNotificationsAsSeenCommand request, CancellationToken cancellationToken)
        {


            var repository = _unitOfWork.GetRepository<UserNotification, int>();


            var notifications = await repository.FindAsync(n => n.UserId == request.userId && n.IsSeen == false
           , false);


            if (!notifications.Any())
                return "No unread notifications found.";


            foreach (var notification in notifications)
            {

                notification.IsSeen = true;
            }


            repository.UpdateRange(notifications);


            await _unitOfWork.Commit();

            return $"{notifications.Count()} notifications marked as seen.";

        }
    }
}
