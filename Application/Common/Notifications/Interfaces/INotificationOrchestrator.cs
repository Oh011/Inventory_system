using Domain.Events.Products;
using Domain.Events.PurchaseOrder;

namespace Project.Application.Common.Notifications.Interfaces
{
    public interface INotificationOrchestrator
    {


        public Task NotifyPurchaseOrderStatusChangeAsync(PurchaseOrderStatusChangedDomainEvent domainEvent);



        public Task NotifyLowStockAsync(ProductStockDecreasedEvent domainEvent);

    }
}
