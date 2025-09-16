using Domain.Entities;
using Domain.Specifications;
using InventorySystem.Application.Common.Enums.SortOptions;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.Inventory.Queries.GetInventory;

namespace InventorySystem.Application.Features.Inventory.Specifications
{
    internal class InventoryProductSpecifications : ProjectionSpecifications<Product, InventoryDto>
    {


        public InventoryProductSpecifications(GetInventoryQuery query)
       : base(p =>
           (string.IsNullOrWhiteSpace(query.Name) || p.Name.ToLower().Contains(query.Name.ToLower())) &&
           (!query.CategoryId.HasValue || p.CategoryId == query.CategoryId.Value) &&
           (!query.MinStock.HasValue || p.QuantityInStock >= query.MinStock.Value) &&
           (!query.MaxStock.HasValue || p.QuantityInStock <= query.MaxStock.Value)
       )
        {




            AddProjection(p => new InventoryDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                CurrentStock = p.QuantityInStock,
                Unit = p.Unit.ToString(),
                CategoryName = p.Category != null ? p.Category.Name : null
            });


            switch (query.InventoryProductSortOptions)
            {
                case InventoryProductSortOptions.QuantityInStockAsc:
                    SetOrderBy(p => p.QuantityInStock);
                    break;

                case InventoryProductSortOptions.QuantityInStockDesc:
                    SetOrderByDescending(p => p.QuantityInStock);
                    break;

                default:
                    SetOrderByDescending(p => p.QuantityInStock); // Fallback: show items with most stock
                    break;
            }



            ApplyPagination(query.PageIndex, query.pageSize);
        }
    }
}
