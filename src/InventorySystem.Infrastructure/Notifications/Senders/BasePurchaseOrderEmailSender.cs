using Domain.Common;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using InventorySystem.Application.Common.Factories.Interfaces;
using InventorySystem.Application.Common.Interfaces.PdfGenerators;
using InventorySystem.Application.Common.Notifications.Senders;
using InventorySystem.Application.Features.PurchaseOrders.Interfaces;

namespace Infrastructure.Notifications.Senders
{
    public abstract class BasePurchaseOrderEmailSender<TEvent> : INotificationSender<TEvent>
    where TEvent : IDomainEvent
    {
        protected readonly IEmailService _emailService;
        protected readonly IPurchaseOrderService _purchaseOrderService;
        protected readonly IPurchaseOrderPdfGenerator _pdfGenerator;
        protected readonly IEmailMessageFactory _emailFactory;

        protected BasePurchaseOrderEmailSender(IEmailService emailService, IPurchaseOrderService purchaseOrderService,
            IPurchaseOrderPdfGenerator pdfGenerator, IEmailMessageFactory emailFactory)
        {
            _emailService = emailService;
            _purchaseOrderService = purchaseOrderService;
            _pdfGenerator = pdfGenerator;
            _emailFactory = emailFactory;
        }

        public abstract Task SendAsync(TEvent domainEvent);
    }

}
