using MediatR;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Features.Products.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace Project.Application.Features.Products.queries.GetProductsForSupplier
{


    public class GetPurchaseProductsLookupQuery : PaginationQueryParameters, IRequest<PaginatedResult<PurchaseProductDto>>
    {
        public int SupplierId { get; set; }

        public string? Name { get; set; }


        public int? CategoryId { get; init; }


        public ProductSortOptions? SortOptions { get; set; }


    }

}
