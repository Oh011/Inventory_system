using MediatR;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Features.Products.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace Project.Application.Features.Products.queries.GetSalesProducts
{
    public class GetSalesProductsLookupQuery : PaginationQueryParameters, IRequest<PaginatedResult<SalesProductDto>>
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }

        public string? Barcode { get; set; }
        public ProductSortOptions? SortOptions { get; set; }
    }

}
