using Domain.Events.PurchaseOrder;
using MediatR;
using Project.Application.Common.Events;
using Project.Application.Common.Interfaces.Background;
using Project.Application.Common.Notifications.Interfaces;

namespace Project.Application.Features.PurchaseOrders.EventHandlers.Created
{
    public class SendPurchaseOrderCreatedEmailHandler
     : INotificationHandler<DomainEventNotifications<PurchaseOrderCreatedDomainEvent>>
    {

        private readonly IBackgroundJobService _backgroundJobService;





        public SendPurchaseOrderCreatedEmailHandler(IBackgroundJobService backgroundJobService)
        {

            _backgroundJobService = backgroundJobService;



        }

        public async Task Handle(DomainEventNotifications<PurchaseOrderCreatedDomainEvent> notification, CancellationToken cancellationToken)
        {



            _backgroundJobService.Enqueue<INotificationOrchestrator<PurchaseOrderCreatedDomainEvent>>(


                e => e.Notify(notification.DomainEvent));

        }
    }

}
