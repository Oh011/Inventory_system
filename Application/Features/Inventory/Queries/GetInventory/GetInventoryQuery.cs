using MediatR;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Features.Inventory.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace Project.Application.Features.Inventory.Queries.GetInventory
{
    public class GetInventoryQuery : PaginationQueryParameters, IRequest<PaginatedResult<InventoryDto>>
    {
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public int? MinStock { get; set; }
        public int? MaxStock { get; set; }


        public InventoryProductSortOptions? InventoryProductSortOptions { get; set; }


    }
}
