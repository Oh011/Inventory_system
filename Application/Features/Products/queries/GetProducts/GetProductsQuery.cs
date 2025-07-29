using MediatR;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Features.Products.Dtos;
using Shared.Parameters;
using Shared.Results;

namespace Project.Application.Features.Products.queries.GetProducts
{
    public class GetProductsQuery : PaginationQueryParameters, IRequest<PaginatedResult<ProductResultDto>>
    {


        public string? Name { get; init; }
        public int? CategoryId { get; init; }
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }


        public ProductSortOptions? sortOptions { get; set; }
    }
}
