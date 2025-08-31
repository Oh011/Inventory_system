using Domain.Common;
using Project.Application.Common.Factories;
using Project.Application.Common.Interfaces.PdfGenerators;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Common.Notifications.Senders;
using Project.Application.Features.PurchaseOrders.Interfaces;

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
