
using Domain.Events.PurchaseOrder;
using MediatR;
using Project.Application.Common.Events;
using Project.Application.Common.Factories;
using Project.Application.Common.Interfaces.Background;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Common.Notifications.Interfaces;

namespace Project.Application.Features.PurchaseOrders.EventHandlers.Cancel
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
