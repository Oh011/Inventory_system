using MediatR;
using InventorySystem.Application.Common.Enums.SortOptions;
using InventorySystem.Application.Features.Products.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace InventorySystem.Application.Features.Products.queries.GetProductsForSupplier
{


    public class GetPurchaseProductsLookupQuery : PaginationQueryParameters, IRequest<PaginatedResult<ProductPurchaseOrderLookUpDto>>
    {
        public int SupplierId { get; set; }

        public string? Name { get; set; }


        public int? CategoryId { get; init; }


        public ProductSortOptions? SortOptions { get; set; }


    }

}
