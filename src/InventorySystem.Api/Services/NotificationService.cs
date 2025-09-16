using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using InventorySystem.RealTime;
using Microsoft.AspNetCore.SignalR;
using InventorySystem.Application.Features.Notifications.Dtos;

namespace Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyLowStockProducts(LowStockNotificationGroup group)
        {
            await _hubContext.Clients.Group("Warehouse").SendAsync("LowStock", group);
            await _hubContext.Clients.Group("Managers").SendAsync("LowStock", group);
            await _hubContext.Clients.Group("Admins").SendAsync("LowStock", group);
        }

        public async Task NotifyPurchaseOrderStatusChangedAsync(UserNotificationDto notification)
        {

            await _hubContext.Clients.Group("Warehouse").SendAsync("PurchaseOrderStatusChanged", notification);
            await _hubContext.Clients.Group("Managers").SendAsync("PurchaseOrderStatusChanged", notification);
            await _hubContext.Clients.Group("Admins").SendAsync("PurchaseOrderStatusChanged", notification);
        }
    }
}


//NotificationHub is a SignalR hub you created that handles connections.

//IHubContext<NotificationHub> is a special service provided by the SignalR framework to let external 
//services (like your NotificationService) interact with that hub.


//public async Task SendRealTimeEventAsync<T>(string eventName, T payload, params string[] groups)
//{
//    foreach (var group in groups)
//    {
//        await _hubContext.Clients.Group(group).SendAsync(eventName, payload);
//    }
//}
