using Domain.Events.PurchaseOrder;
using MediatR;
using Project.Application.Common.Events;
using Project.Application.Common.Factories;
using Project.Application.Common.Interfaces.Background;
using Project.Application.Common.Interfaces.PdfGenerators;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Features.PurchaseOrders.Interfaces;

namespace Project.Application.Features.PurchaseOrders.EventHandlers.Created
{
    public class SendPurchaseOrderCreatedEmailHandler
     : INotificationHandler<DomainEventNotifications<PurchaseOrderCreatedDomainEvent>>
    {
        private readonly IEmailService _emailService;
        private IEmailMessageFactory _emailMessageFactory;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IPurchaseOrderPdfGenerator _pdfGenerator;
        private readonly IBackgroundJobService _backgroundJobService;



        public SendPurchaseOrderCreatedEmailHandler(IBackgroundJobService backgroundJobService, IEmailService emailService, IEmailMessageFactory emailMessageFactory, IPurchaseOrderService purchaseOrderService, IPurchaseOrderPdfGenerator pdfGenerator)
        {
            _emailService = emailService;
            _backgroundJobService = backgroundJobService;
            _pdfGenerator = pdfGenerator;
            this._emailMessageFactory = emailMessageFactory;
            this.purchaseOrderService = purchaseOrderService;


        }

        public async Task Handle(DomainEventNotifications<PurchaseOrderCreatedDomainEvent> notification, CancellationToken cancellationToken)
        {
            var orderId = notification.DomainEvent.PurchaseOrderId;
            var supplierEmail = notification.DomainEvent.SupplierEmail;
            var supplierName = notification.DomainEvent.SupplerName;



            var order = await purchaseOrderService.GetPurchaseOrderById(orderId);

            var pdfBytes = _pdfGenerator.GenerateCreatedOrderPdf(order);



            var emailMessage = _emailMessageFactory.CreatePurchaseOrderEmail(orderId, supplierName, supplierEmail, pdfBytes);


            _backgroundJobService.Enqueue<IEmailService>(e => e.SendEmailAsync(emailMessage));

        }
    }

}
