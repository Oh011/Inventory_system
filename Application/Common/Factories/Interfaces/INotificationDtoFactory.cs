using Domain.Events.PurchaseOrder;
using Project.Application.Features.Inventory.Dtos;
using Project.Application.Features.Notifications.Dtos;

namespace Project.Application.Common.Factories.Interfaces
{
    public interface INotificationDtoFactory
    {
        CreateUserNotificationDto CreatePurchaseOrderStatusNotification(PurchaseOrderStatusChangedDomainEvent domainEvent, List<string> userIds);
        CreateUserNotificationDto CreateLowStockNotification(LowStockProductDto product, List<string> userIds);
    }

}
