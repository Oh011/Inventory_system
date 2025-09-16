using Domain.Events.PurchaseOrder;
using MediatR;
using InventorySystem.Application.Common.Events;
using InventorySystem.Application.Common.Interfaces.Background;
using InventorySystem.Application.Common.Notifications.Interfaces;

namespace InventorySystem.Application.Features.PurchaseOrders.EventHandlers
{
    public class BroadcastPurchaseOrderPurchaseOrderStatusChangedNotificationHandler
     : INotificationHandler<DomainEventNotifications<PurchaseOrderStatusChangedDomainEvent>>
    {



        private readonly IBackgroundJobService _backgroundJobService;

        public BroadcastPurchaseOrderPurchaseOrderStatusChangedNotificationHandler(IBackgroundJobService backgroundJobService
         )
        {


            _backgroundJobService = backgroundJobService;
        }

        public async Task Handle(DomainEventNotifications<PurchaseOrderStatusChangedDomainEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _backgroundJobService.Enqueue<INotificationOrchestrator<PurchaseOrderStatusChangedDomainEvent>>(s => s.Notify(domainEvent));

        }
    }

}
