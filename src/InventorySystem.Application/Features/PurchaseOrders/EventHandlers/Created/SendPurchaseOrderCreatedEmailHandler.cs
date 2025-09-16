using Domain.Events.PurchaseOrder;
using MediatR;
using InventorySystem.Application.Common.Events;
using InventorySystem.Application.Common.Interfaces.Background;
using InventorySystem.Application.Common.Notifications.Interfaces;

namespace InventorySystem.Application.Features.PurchaseOrders.EventHandlers.Created
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
