
using Domain.Events.PurchaseOrder;
using MediatR;
using Project.Application.Common.Events;
using Project.Application.Common.Factories;
using Project.Application.Common.Interfaces.Background;
using Project.Application.Common.Interfaces.Services;

namespace Project.Application.Features.PurchaseOrders.EventHandlers.Cancel
{
    public class SendPurchaseOrderCanceledEmailHandler
    : INotificationHandler<DomainEventNotifications<PurchaseOrderCanceledDomainEvent>>
    {
        private readonly IEmailService _emailService;
        private readonly IEmailMessageFactory _emailMessageFactory;
        private readonly IBackgroundJobService _backgroundJobService;

        public SendPurchaseOrderCanceledEmailHandler(IBackgroundJobService backgroundJobService, IEmailService emailService, IEmailMessageFactory emailMessageFactory)
        {
            _emailService = emailService;
            _backgroundJobService = backgroundJobService;
            _emailMessageFactory = emailMessageFactory;
        }

        public async Task Handle(DomainEventNotifications<PurchaseOrderCanceledDomainEvent> notification, CancellationToken cancellationToken)
        {

            var supplierName = notification.DomainEvent.SupplerName;
            var supplierEmail = notification.DomainEvent.SupplierEmail;
            var orderId = notification.DomainEvent.PurchaseOrderId;

            var email = _emailMessageFactory.CreateOrderCanceledEmail(orderId, supplierName, supplierEmail);


            _backgroundJobService.Enqueue<IEmailService>(e => e.SendEmailAsync(email));

        }
    }
}
