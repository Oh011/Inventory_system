using Domain.Entities;
using Domain.Specifications;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Features.Inventory.Dtos;
using Project.Application.Features.Inventory.Queries.GetLowStock;

namespace Project.Application.Features.Inventory.Specifications
{
    public class LowStockInventorySpecification : ProjectionSpecifications<Product, LowStockProductDto>
    {


        public LowStockInventorySpecification(GetLowStockQuery query) : base(p =>
            (string.IsNullOrEmpty(query.Name) || p.Name.Contains(query.Name)) &&
            (!query.CategoryId.HasValue || p.CategoryId == query.CategoryId.Value) &&
            (query.OnlyCritical == true ? p.QuantityInStock <= 2 : p.QuantityInStock <= 10)
        && (p.QuantityInStock < p.MinimumStock)
        )
        {


            AddProjection(p => new LowStockProductDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                Unit = p.Unit.ToString(),
                QuantityInStock = p.QuantityInStock,
                ReorderThreshold = p.MinimumStock,
                CategoryName = p.Category != null ? p.Category.Name : null
            });

            switch (query.ProductSortOptions)
            {
                case InventoryProductSortOptions.QuantityInStockAsc:
                    SetOrderBy(p => p.QuantityInStock);
                    break;

                case InventoryProductSortOptions.QuantityInStockDesc:
                    SetOrderByDescending(p => p.QuantityInStock);
                    break;

                default:
                    SetOrderBy(p => p.QuantityInStock); // Default sort
                    break;
            }
        }
    }

}
