using Domain.Events.Products;
using InventorySystem.Application.Common.Helpers;
using InventorySystem.Application.Common.Interfaces;

namespace InventorySystem.Services
{
    public interface IStockEventService
    {
        Task RaiseStockDecreasedEventAsync(IEnumerable<int> productIds);
    }

    public class StockEventService : IStockEventService
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public StockEventService(IDomainEventDispatcher domainEventDispatcher)
        {
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task RaiseStockDecreasedEventAsync(IEnumerable<int> productIds)
        {
            if (productIds == null || !productIds.Any()) return;

            var stockDecreasedEvent = new ProductStockDecreasedEvent(productIds.ToList());
            await EventDispatcherHelper.DispatchOnly(stockDecreasedEvent, _domainEventDispatcher);
        }
    }

}
