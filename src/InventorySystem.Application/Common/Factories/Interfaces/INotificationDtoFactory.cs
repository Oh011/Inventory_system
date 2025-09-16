using Domain.Events.PurchaseOrder;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.Notifications.Dtos;

namespace InventorySystem.Application.Common.Factories.Interfaces
{
    public interface INotificationDtoFactory
    {
        CreateUserNotificationDto CreatePurchaseOrderStatusNotification(PurchaseOrderStatusChangedDomainEvent domainEvent, List<string> userIds);
        CreateUserNotificationDto CreateLowStockNotification(LowStockProductDto product, List<string> userIds);
    }

}
