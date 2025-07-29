using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Project.Application.Features.Notifications.Dtos;
using Project.Application.Features.Notifications.Queries.GetUserNotifications;

using System.Security.Claims;

namespace InventorySystem.RealTime
{
    public class NotificationHub : Hub
    {


        private readonly IMediator mediator;


        public NotificationHub(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize]
        public async override Task OnConnectedAsync()
        {

            var user = Context.User;



            if (user.IsInRole("Admin"))
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");

            if (user.IsInRole("Manager"))
                await Groups.AddToGroupAsync(Context.ConnectionId, "Managers");

            if (user.IsInRole("Warehouse"))
                await Groups.AddToGroupAsync(Context.ConnectionId, "Warehouse");


            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

            var query = new GetUserNotificationsQuery(userId, false)
            {
                pageSize = 7,
                PageIndex = 1


            };
            var messages = await mediator.Send(query);


            if (messages != null && messages.Items.Any())
            {
                // ✅ Send them to the connected user
                await Clients.Caller.SendAsync("ReceiveNotifications", messages.Items);
            }

            else
            {

                await Clients.Caller.SendAsync("ReceiveNotifications", new List<UserNotificationDto>());
            }


            await base.OnConnectedAsync();

        }



    }
}
