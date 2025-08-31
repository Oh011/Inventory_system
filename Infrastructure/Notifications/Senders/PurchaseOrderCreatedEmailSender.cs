using Domain.Events.PurchaseOrder;
using Project.Application.Common.Factories;
using Project.Application.Common.Interfaces.PdfGenerators;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Common.Notifications.Senders;
using Project.Application.Features.PurchaseOrders.Interfaces;

namespace Infrastructure.Notifications.Senders
{
    public class PurchaseOrderCreatedEmailSender : INotificationSender<PurchaseOrderCreatedDomainEvent>
    {

        private readonly IEmailService _emailService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IPurchaseOrderPdfGenerator _pdfGenerator;
        private readonly IEmailMessageFactory _emailFactory;

        public PurchaseOrderCreatedEmailSender(IEmailService emailService, IPurchaseOrderService purchaseOrderService,
            IPurchaseOrderPdfGenerator pdfGenerator, IEmailMessageFactory emailFactory)
        {
            _emailService = emailService;
            _purchaseOrderService = purchaseOrderService;
            _pdfGenerator = pdfGenerator;
            _emailFactory = emailFactory;
        }

        public async Task SendAsync(PurchaseOrderCreatedDomainEvent domainEvent)
        {
            var order = await _purchaseOrderService.GetPurchaseOrderById(domainEvent.PurchaseOrderId);
            var pdf = _pdfGenerator.GenerateCreatedOrderPdf(order);
            var email = _emailFactory.CreatePurchaseOrderCreatedEmail(order.Id, domainEvent.SupplierName, domainEvent.SupplierEmail, pdf);
            await _emailService.SendEmailAsync(email);
        }
    }

}
