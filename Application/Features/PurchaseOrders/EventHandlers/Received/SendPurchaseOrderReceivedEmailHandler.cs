using Domain.Events.PurchaseOrder;
using MediatR;
using Project.Application.Common.Events;
using Project.Application.Common.Factories;
using Project.Application.Common.Interfaces.Background;
using Project.Application.Common.Interfaces.PdfGenerators;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Features.PurchaseOrders.Interfaces;

namespace Project.Application.Features.PurchaseOrders.EventHandlers.Received
{
    internal class SendPurchaseOrderReceivedEmailHandler : INotificationHandler<DomainEventNotifications<PurchaseOrderReceivedDomainEvent>>
    {
        private readonly IEmailService _emailService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IPurchaseOrderPdfGenerator _pdfGenerator;
        private readonly IEmailMessageFactory _emailFactory;
        private readonly IBackgroundJobService _backgroundJobService;


        public SendPurchaseOrderReceivedEmailHandler(
            IBackgroundJobService backgroundJobService,
       IEmailService emailService,
       IPurchaseOrderService purchaseOrderService,
       IPurchaseOrderPdfGenerator pdfGenerator,
       IEmailMessageFactory emailFactory)
        {
            _emailService = emailService;
            _backgroundJobService = backgroundJobService;
            _purchaseOrderService = purchaseOrderService;
            _pdfGenerator = pdfGenerator;
            _emailFactory = emailFactory;
        }


        public async Task Handle(DomainEventNotifications<PurchaseOrderReceivedDomainEvent> notification, CancellationToken cancellationToken)
        {

            var orderId = notification.DomainEvent.PurchaseOrderId;
            var supplierEmail = notification.DomainEvent.SupplierEmail;
            var supplierName = notification.DomainEvent.SupplerName;



            var order = await _purchaseOrderService.GetPurchaseOrderById(orderId);

            var pdfBytes = _pdfGenerator.GenerateReceivedOrderPdf(order);



            var emailMessage = _emailFactory.CreatePurchaseOrderEmail(orderId, supplierName, supplierEmail, pdfBytes);


            _backgroundJobService.Enqueue<IEmailService>(e => e.SendEmailAsync(emailMessage));


        }
    }
}
