using Domain.Events.PurchaseOrder;
using MediatR;
using Project.Application.Common.Events;
using Project.Application.Common.Interfaces.Background;
using Project.Application.Common.Notifications.Interfaces;

namespace Project.Application.Features.PurchaseOrders.EventHandlers
{
    public class BroadcastPurchaseOrderPurchaseOrderStatusChangedNotificationHandler
     : INotificationHandler<DomainEventNotifications<PurchaseOrderStatusChangedDomainEvent>>
    {

        private readonly INotificationOrchestrator _notificationOrchestrator;
        private readonly IBackgroundJobService _backgroundJobService;

        public BroadcastPurchaseOrderPurchaseOrderStatusChangedNotificationHandler(IBackgroundJobService backgroundJobService,
          INotificationOrchestrator notificationOrchestrator)
        {


            _notificationOrchestrator = notificationOrchestrator;
            _backgroundJobService = backgroundJobService;
        }

        public async Task Handle(DomainEventNotifications<PurchaseOrderStatusChangedDomainEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;



            _backgroundJobService.Enqueue<INotificationOrchestrator>(s => s.NotifyPurchaseOrderStatusChangeAsync(domainEvent));
        }
    }

}
