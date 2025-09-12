using Domain.Entities;
using Domain.Events.Products;
using Project.Application.Common.Helpers;
using Project.Application.Common.Interfaces;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Features.Inventory.Dtos;
using Project.Application.Features.Products.Intrefaces;
using ValidationException = Application.Exceptions.ValidationException;

namespace Project.Services
{
    internal class InventoryService(IProductRepository productRepository, IUnitOfWork _unitOfWork, IDomainEventDispatcher domainEventDispatcher, IConcurrencyHelper _concurrencyHelper) : IInventoryService
    {
        public async Task AdjustStockAsync(List<InventoryStockAdjustmentDto> adjustments, ITransactionManager transactionManager)
        {


            var repository = _unitOfWork.GetRepository<Product, int>();
            var productIds = adjustments.Select(x => x.ProductId).ToList();

            var receivedItems = adjustments.ToDictionary(i => i.ProductId, i => i.QuantityChange);


            //var products = await repository.FindAsync(p => productIds.Contains(p.Id));
            Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

            var decreasedProductIds = new List<int>();



            foreach (var product in receivedItems)
            {


                int productId = product.Key;
                var affectedRows = await productRepository.AdjustProductStock(productId, product.Value);



                if (affectedRows.NewQuantity == affectedRows.OldQuantity)
                {
                    if (!errors.ContainsKey(productId.ToString()))
                        errors[productId.ToString()] = new List<string>();

                    errors[productId.ToString()].Add($"Inssuftent quantity orderd{Math.Abs(product.Value)} available :{affectedRows.OldQuantity} ");
                }



                if (affectedRows.NewQuantity < affectedRows.Threshold)
                    decreasedProductIds.Add(product.Key);


            }





            if (errors.Any())
                throw new ValidationException(errors);


            await _unitOfWork.Commit();
            await transactionManager.CommitTransaction();


            if (decreasedProductIds.Any())
            {
                var stockDecreasedEvent = new ProductStockDecreasedEvent(decreasedProductIds);
                await EventDispatcherHelper.DispatchOnly(stockDecreasedEvent, domainEventDispatcher);
            }
        }
    }
}
