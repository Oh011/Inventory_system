
using Domain.Events.PurchaseOrder;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using MediatR;
using InventorySystem.Application.Common.Events;
using InventorySystem.Application.Common.Factories.Interfaces;
using InventorySystem.Application.Common.Interfaces.Background;
using InventorySystem.Application.Common.Notifications.Interfaces;

namespace InventorySystem.Application.Features.PurchaseOrders.EventHandlers.Cancel
{
    public class SendPurchaseOrderCanceledEmailHandler
    : INotificationHandler<DomainEventNotifications<PurchaseOrderCanceledDomainEvent>>
    {

        private readonly IBackgroundJobService _backgroundJobService;

        public SendPurchaseOrderCanceledEmailHandler(IBackgroundJobService backgroundJobService, IEmailService emailService, IEmailMessageFactory emailMessageFactory)
        {

            _backgroundJobService = backgroundJobService;

        }

        public async Task Handle(DomainEventNotifications<PurchaseOrderCanceledDomainEvent> notification, CancellationToken cancellationToken)
        {



            _backgroundJobService.Enqueue<INotificationOrchestrator<
                PurchaseOrderCanceledDomainEvent>>(e => e.Notify(notification.DomainEvent));

        }
    }
}
