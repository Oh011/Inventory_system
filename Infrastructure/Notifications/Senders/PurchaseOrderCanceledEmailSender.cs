using Domain.Events.PurchaseOrder;
using Project.Application.Common.Factories;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Common.Notifications.Senders;

namespace Infrastructure.Notifications.Senders
{
    internal class PurchaseOrderCanceledEmailSender : INotificationSender<PurchaseOrderCanceledDomainEvent>
    {

        private readonly IEmailService _emailService;
        private readonly IEmailMessageFactory _emailMessageFactory;


        public PurchaseOrderCanceledEmailSender(IEmailService emailService, IEmailMessageFactory emailMessageFactory)
        {
            _emailService = emailService;
            _emailMessageFactory = emailMessageFactory;
        }

        public async Task SendAsync(PurchaseOrderCanceledDomainEvent domainEvent)
        {
            var supplierName = domainEvent.SupplierName;
            var supplierEmail = domainEvent.SupplierEmail;
            var orderId = domainEvent.PurchaseOrderId;

            var email = _emailMessageFactory.CreateOrderCanceledEmail(orderId, supplierName, supplierEmail);

            await _emailService.SendEmailAsync(email);
        }
    }
}
