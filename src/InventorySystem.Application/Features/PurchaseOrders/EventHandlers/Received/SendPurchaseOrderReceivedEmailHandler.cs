using Domain.Events.PurchaseOrder;
using MediatR;
using InventorySystem.Application.Common.Events;
using InventorySystem.Application.Common.Interfaces.Background;
using InventorySystem.Application.Common.Notifications.Interfaces;

namespace InventorySystem.Application.Features.PurchaseOrders.EventHandlers.Received
{
    internal class SendPurchaseOrderReceivedEmailHandler : INotificationHandler<DomainEventNotifications<PurchaseOrderReceivedDomainEvent>>
    {


        private readonly IBackgroundJobService _backgroundJobService;





        public SendPurchaseOrderReceivedEmailHandler(IBackgroundJobService backgroundJobService)
        {

            _backgroundJobService = backgroundJobService;



        }

        public async Task Handle(DomainEventNotifications<PurchaseOrderReceivedDomainEvent> notification, CancellationToken cancellationToken)
        {




            _backgroundJobService.Enqueue<INotificationOrchestrator<
                PurchaseOrderReceivedDomainEvent>>(e => e.Notify(notification.DomainEvent));


        }
    }
}
