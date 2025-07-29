using Application.Exceptions;
using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.Repositories;

namespace Project.Application.Features.Notifications.Commands.MarkNotificationAsSeen
{
    internal class MarkNotificationAsSeenCommandHandler
      : IRequestHandler<MarkNotificationAsSeenCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarkNotificationAsSeenCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(MarkNotificationAsSeenCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<UserNotification, int>();

            var userNotification = (await repository.FindAsync(
                un => un.NotificationId == request.NotificationId && un.UserId == request.UserId

            )).FirstOrDefault();

            if (userNotification is null)
                throw new NotFoundException("Notification not found for this user.");



            userNotification.IsSeen = true;

            repository.Update(userNotification);

            await _unitOfWork.Commit(cancellationToken);

            return $"Notification with Id :{request.NotificationId} marked as seen";
        }
    }

}
