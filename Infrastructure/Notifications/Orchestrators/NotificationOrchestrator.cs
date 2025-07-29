using Domain.Events.Products;
using Domain.Events.PurchaseOrder;
using Project.Application.Common.Notifications.Interfaces;
using Project.Application.Common.Notifications.Senders;

namespace Infrastructure.Notifications.Orchestrators
{
    public class NotificationOrchestrator : INotificationOrchestrator
    {
        private readonly INotificationSender<PurchaseOrderStatusChangedDomainEvent> _poStatusSender;
        private readonly INotificationSender<ProductStockDecreasedEvent> _lowStockSender;

        public NotificationOrchestrator(
            INotificationSender<PurchaseOrderStatusChangedDomainEvent> poStatusSender,
            INotificationSender<ProductStockDecreasedEvent> lowStockSender)
        {
            _poStatusSender = poStatusSender;
            _lowStockSender = lowStockSender;
        }

        public Task NotifyPurchaseOrderStatusChangeAsync(PurchaseOrderStatusChangedDomainEvent domainEvent)
            => _poStatusSender.SendAsync(domainEvent);

        public Task NotifyLowStockAsync(ProductStockDecreasedEvent domainEvent)
            => _lowStockSender.SendAsync(domainEvent);
    }

}
