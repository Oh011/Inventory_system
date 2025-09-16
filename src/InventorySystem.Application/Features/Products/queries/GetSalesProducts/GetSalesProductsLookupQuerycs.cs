using MediatR;
using InventorySystem.Application.Common.Enums.SortOptions;
using InventorySystem.Application.Features.Products.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace InventorySystem.Application.Features.Products.queries.GetSalesProducts
{
    public class GetSalesProductsLookupQuery : PaginationQueryParameters, IRequest<PaginatedResult<ProductSalesLookupDto>>
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }

        public string? Barcode { get; set; }
        public ProductSortOptions? SortOptions { get; set; }
    }

}
