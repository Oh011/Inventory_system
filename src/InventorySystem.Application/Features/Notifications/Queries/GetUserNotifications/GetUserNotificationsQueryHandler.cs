using MediatR;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Features.Notifications.Dtos;
using Project.Application.Features.Notifications.Interfaces;
using Shared.Results;

namespace Project.Application.Features.Notifications.Queries.GetUserNotifications
{
    internal class GetUserNotificationsQueryHandler : IRequestHandler<GetUserNotificationsQuery, PaginatedResult<UserNotificationDto>>
    {



        private readonly IUserNotificationsService _userNotificationsService;
        private readonly IAuthorizationService _authorizationService;




        public GetUserNotificationsQueryHandler(IUserNotificationsService userNotificationsService, IAuthorizationService authorizationService)
        {

            _userNotificationsService = userNotificationsService;
            _authorizationService = authorizationService;

        }
        public async Task<PaginatedResult<UserNotificationDto>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
        {

            _authorizationService.EnsureSelf(request.userId);


            var result = await _userNotificationsService.GetUserNotifications(request);

            return result;
        }
    }
}
