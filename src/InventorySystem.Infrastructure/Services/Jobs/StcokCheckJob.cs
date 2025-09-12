using Domain.Entities;
using Domain.Events.Products;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Common.Notifications.Senders;

namespace Infrastructure.Services.Jobs
{
    public class DailyStockCheckJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationSender<ProductStockDecreasedEvent> _sender;

        public DailyStockCheckJob(IUnitOfWork unitOfWork, INotificationSender<ProductStockDecreasedEvent> sender)
        {
            _unitOfWork = unitOfWork;
            _sender = sender;
        }

        public async Task RunAsync()
        {
            var repository = _unitOfWork.GetRepository<Product, int>();

            var allLowStockIds = await repository.ListAsync(
                p => p.QuantityInStock < p.MinimumStock,
                p => p.Id);


            if (allLowStockIds.Any())
            {
                var domainEvent = new ProductStockDecreasedEvent(allLowStockIds);


                await _sender.SendAsync(domainEvent);

            }
        }
    }
}
