using Domain.Entities;
using InventorySystem.Application.Common.Interfaces;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using InventorySystem.Application.Common.Validation;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.Products.Intrefaces;
using Shared.Errors;
using ValidationException = Application.Exceptions.ValidationException;

namespace InventorySystem.Services
{
    internal class InventoryService(IProductRepository productRepository, IUnitOfWork _unitOfWork, IDomainEventDispatcher domainEventDispatcher, IConcurrencyHelper _concurrencyHelper) : IInventoryService
    {
        public async Task<IEnumerable<int>> AdjustStockAsync(List<InventoryStockAdjustmentDto> adjustments, ITransactionManager transactionManager)
        {


            var repository = _unitOfWork.GetRepository<Product, int>();
            var productIds = adjustments.Select(x => x.ProductId).ToList();

            var receivedItems = adjustments.ToDictionary(i => i.ProductId, i => i.QuantityChange);


            Dictionary<string, List<ValidationErrorDetail>> errors = new Dictionary<string, List<ValidationErrorDetail>>();


            var decreasedProductIds = new List<int>();



            foreach (var product in receivedItems)
            {

                int change = product.Value;
                int productId = product.Key;



                var result = await productRepository.AdjustProductStock(productId, product.Value);



                if (result.NewQuantity == result.OldQuantity)
                {
                    if (!errors.ContainsKey(productId.ToString()))
                        errors["Products"] = new List<ValidationErrorDetail>();



                    errors["Products"].Add(new ProductStockErrorDetail
                    (
                         product.Key,
                       Math.Abs(change),
                         result.OldQuantity));



                }




                if (result.NewQuantity < result.Threshold)
                    decreasedProductIds.Add(product.Key);


            }





            if (errors.Any())
                throw new ValidationException(errors);



            return decreasedProductIds;


        }
    }
}
