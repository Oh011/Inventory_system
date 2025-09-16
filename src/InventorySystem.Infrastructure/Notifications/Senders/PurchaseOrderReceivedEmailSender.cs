using Domain.Events.PurchaseOrder;
using InventorySystem.Application.Common.Factories.Interfaces;
using InventorySystem.Application.Common.Interfaces.PdfGenerators;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using InventorySystem.Application.Features.PurchaseOrders.Interfaces;

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
            var email = _emailFactory.CreateOrderReceivedEmail(order.Id, domainEvent.SupplierName, domainEvent.SupplierEmail, domainEvent.Status, pdf);
            await _emailService.SendEmailAsync(email);
        }
    }
}
