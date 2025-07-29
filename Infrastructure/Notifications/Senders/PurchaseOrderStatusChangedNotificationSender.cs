using Application.Features.Users.Interfaces;
using Domain.Events.PurchaseOrder;
using Project.Application.Common.Factories;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Common.Notifications.Senders;
using Project.Application.Features.Notifications.Interfaces;

namespace Infrastructure.Notifications.Senders
{
    public class PurchaseOrderStatusChangedNotificationSender(INotificationDtoFactory notificationDtoFactory, INotificationService _notificationService, IUserService _userService, IUserNotificationsService _userNotificationsService) : INotificationSender<PurchaseOrderStatusChangedDomainEvent>
    {
        // Inject services like UserService, NotificationService, etc.

        public async Task SendAsync(PurchaseOrderStatusChangedDomainEvent domainEvent)
        {



            var userIds = await _userService.GetUsersIdByRole(new List<string> { "Admin", "Manager", "Warehouse" });


            var dto = notificationDtoFactory.CreatePurchaseOrderStatusNotification(domainEvent, userIds);

            var created = await _userNotificationsService.CreateNotification(dto);
            created.Link = $"/purchase-orders/{domainEvent.PurchaseOrderId}";

            await _notificationService.NotifyPurchaseOrderStatusChangedAsync(created);
        }
    }

}
