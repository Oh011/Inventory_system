using MediatR;
using InventorySystem.Application.Common.Enums.SortOptions;
using InventorySystem.Application.Features.Inventory.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace InventorySystem.Application.Features.Inventory.Queries.GetLowStock
{
    public class GetLowStockQuery : PaginationQueryParameters, IRequest<PaginatedResult<LowStockProductDto>>
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public bool? OnlyCritical { get; set; } // e.g., stock = 0 or very low

        public InventoryProductSortOptions? ProductSortOptions { get; set; }
    }

}
