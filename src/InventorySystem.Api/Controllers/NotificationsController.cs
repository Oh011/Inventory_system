using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventorySystem.Application.Features.Notifications.Commands.MarkNotificationAsSeen;
using InventorySystem.Application.Features.Notifications.Commands.MarkNotificationsAsSeen;
using InventorySystem.Application.Features.Notifications.Dtos;
using InventorySystem.Application.Features.Notifications.Queries.GetUnseenCount;
using InventorySystem.Application.Features.Notifications.Queries.GetUserNotifications;
using Shared;
using Shared.Results;
using System.Security.Claims;

namespace InventorySystem.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{ version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController(IMediator mediator) : ControllerBase
    {


        /// <summary>
        /// Retrieves the authenticated user's notifications, with optional filter for seen/unseen.
        /// </summary>
        /// <param name="IsSeen">Filter by seen status (true, false, or null for all)</param>
        [HttpGet]
        public async Task<ActionResult<SuccessWithData<PaginatedResult<UserNotificationDto>>>> GetUserNotifications([FromQuery] bool? IsSeen = null)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var query = new GetUserNotificationsQuery(userId, IsSeen);

            var result = await mediator.Send(query);



            return Ok(ApiResponseFactory.Success(result));

        }



        /// <summary>
        /// Marks all of the authenticated user's notifications as seen.
        /// </summary>
        [HttpPatch("mark-all-as-seen")]
        public async Task<ActionResult<SuccessMessage>> MarkAllNotificationsAsSeen()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var command = new MarkAllNotificationsAsSeenCommand(userId);

            var result = await mediator.Send(command);

            return Ok(ApiResponseFactory.Success(result));
        }



        /// <summary>
        /// Gets the count of unseen notifications for the authenticated user.
        /// </summary>

        [HttpGet("unread-count")]

        public async Task<ActionResult<SuccessWithData<NotificationsCount>>> getUnreadNotificationsCount()
        {



            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var query = new GetUnseenNotificationsCountQuery(userId);

            var result = await mediator.Send(query);



            return Ok(ApiResponseFactory.Success(result));


        }


        /// <summary>
        /// Marks a specific notification as seen for the authenticated user.
        /// </summary>
        /// <param name="id">Notification ID</param>

        [HttpPatch("{id}/mark-as-seen")]
        public async Task<ActionResult<SuccessMessage>> MarkNotificationAsSeen(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var command = new MarkNotificationAsSeenCommand(id, userId);

            var result = await mediator.Send(command);

            return Ok(ApiResponseFactory.Success(result));
        }
    }
}
