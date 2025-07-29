using Domain.Events.Products;
using MediatR;
using Project.Application.Common.Events;
using Project.Application.Common.Interfaces.Background;
using Project.Application.Common.Notifications.Interfaces;

namespace Project.Application.Features.Inventory.Handlers
{
    internal class ProductStockDecreasedEventHandler : INotificationHandler<DomainEventNotifications<ProductStockDecreasedEvent>>
    {


        private readonly INotificationOrchestrator notificationOrchestrator;
        private readonly IBackgroundJobService backgroundJobService;

        public ProductStockDecreasedEventHandler(IBackgroundJobService backgroundJobService, INotificationOrchestrator notificationOrchestrator)
        {

            this.notificationOrchestrator = notificationOrchestrator;
            this.backgroundJobService = backgroundJobService;
        }

        public async Task Handle(DomainEventNotifications<ProductStockDecreasedEvent> notification, CancellationToken cancellationToken)
        {



            backgroundJobService.Enqueue<INotificationOrchestrator>(e => e.NotifyLowStockAsync(notification.DomainEvent));



        }
    }

}
