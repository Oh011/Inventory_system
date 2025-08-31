using Domain.Events.PurchaseOrder;
using Project.Application.Common.Factories;
using Project.Application.Common.Interfaces.PdfGenerators;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Features.PurchaseOrders.Interfaces;

namespace Infrastructure.Notifications.Senders
{
    internal class PurchaseOrderReceivedEmailSender : BasePurchaseOrderEmailSender<PurchaseOrderReceivedDomainEvent>
    {
        public PurchaseOrderReceivedEmailSender(IEmailService emailService, IPurchaseOrderService purchaseOrderService, IPurchaseOrderPdfGenerator pdfGenerator, IEmailMessageFactory emailFactory) : base(emailService, purchaseOrderService, pdfGenerator, emailFactory)
        {
        }

        public override async Task SendAsync(PurchaseOrderReceivedDomainEvent domainEvent)
        {
            var order = await _purchaseOrderService.GetPurchaseOrderById(domainEvent.PurchaseOrderId);
            var pdf = _pdfGenerator.GenerateReceivedOrderPdf(order);
            var email = _emailFactory.CreatePurchaseOrderCreatedEmail(order.Id, domainEvent.SupplierName, domainEvent.SupplierEmail, pdf);
            await _emailService.SendEmailAsync(email);
        }
    }
}
