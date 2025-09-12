using Domain.Events.PurchaseOrder;
using MediatR;
using Project.Application.Common.Events;
using Project.Application.Common.Interfaces.Background;
using Project.Application.Common.Notifications.Interfaces;

namespace Project.Application.Features.PurchaseOrders.EventHandlers.Received
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
