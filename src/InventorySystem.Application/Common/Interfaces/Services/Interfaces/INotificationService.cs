using InventorySystem.Application.Features.Notifications.Dtos;

namespace InventorySystem.Application.Common.Interfaces.Services.Interfaces
{
    public interface INotificationService
    {


        Task NotifyPurchaseOrderStatusChangedAsync(UserNotificationDto notification);

        Task NotifyLowStockProducts(LowStockNotificationGroup group);
    }

}
