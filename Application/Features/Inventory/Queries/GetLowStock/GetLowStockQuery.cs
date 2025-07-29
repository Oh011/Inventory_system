using MediatR;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Features.Inventory.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace Project.Application.Features.Inventory.Queries.GetLowStock
{
    public class GetLowStockQuery : PaginationQueryParameters, IRequest<PaginatedResult<LowStockProductDto>>
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public bool? OnlyCritical { get; set; } // e.g., stock = 0 or very low

        public InventoryProductSortOptions? ProductSortOptions { get; set; }
    }

}
