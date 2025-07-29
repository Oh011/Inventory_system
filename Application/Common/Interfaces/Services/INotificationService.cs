using Project.Application.Features.Notifications.Dtos;

namespace Project.Application.Common.Interfaces.Services
{
    public interface INotificationService
    {


        Task NotifyPurchaseOrderStatusChangedAsync(UserNotificationDto notification);

        Task NotifyLowStockProducts(LowStockNotificationGroup group);
    }

}
