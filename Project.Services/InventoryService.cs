using Domain.Entities;
using Domain.Events.Products;
using Project.Application.Common.Helpers;
using Project.Application.Common.Interfaces;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Features.Inventory.Dtos;

namespace Project.Services
{
    internal class InventoryService(IUnitOfWork _unitOfWork, IDomainEventDispatcher domainEventDispatcher, IConcurrencyHelper _concurrencyHelper) : IInventoryService
    {
        public async Task AdjustStockAsync(List<InventoryStockAdjustmentDto> adjustments)
        {


            var repository = _unitOfWork.GetRepository<Product, int>();
            var productIds = adjustments.Select(x => x.ProductId).ToList();

            var receivedItems = adjustments.ToDictionary(i => i.ProductId, i => i.QuantityChange);

            await _concurrencyHelper.ExecuteWithRetryAsync(async () =>
            {
                var products = await repository.FindAsync(p => productIds.Contains(p.Id));

                foreach (var product in products)
                {
                    var Quantity = receivedItems[product.Id];

                    _unitOfWork.EnsureRowVersionMatch(product, Convert.ToBase64String(product.RowVersion));
                    _unitOfWork.ApplyRowVersion(product, Convert.ToBase64String(product.RowVersion));



                    if (Quantity < 0)
                        product.IncreaseStock(Quantity);

                    else if (Quantity >= 0)
                        product.IncreaseStock(Quantity);
                }

                repository.UpdateRange(products);
                await _unitOfWork.Commit(); // commit inside retry
            });


            var decreasedProductIds = adjustments
             .Where(x => x.QuantityChange < 0)
             .Select(x => x.ProductId)
             .Distinct()
             .ToList();

            if (decreasedProductIds.Any())
            {
                var stockDecreasedEvent = new ProductStockDecreasedEvent(decreasedProductIds);
                await EventDispatcherHelper.DispatchOnly(stockDecreasedEvent, domainEventDispatcher);
            }
        }
    }
}
