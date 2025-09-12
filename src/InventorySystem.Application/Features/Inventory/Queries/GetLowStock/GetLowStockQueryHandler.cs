using MediatR;
using Project.Application.Features.Inventory.Dtos;
using Project.Application.Features.Products.Intrefaces;
using Shared.Results;

namespace Project.Application.Features.Inventory.Queries.GetLowStock
{
    internal class GetLowStockQueryHandler : IRequestHandler<GetLowStockQuery, PaginatedResult<LowStockProductDto>>
    {



        private readonly IProductService _productService;


        public GetLowStockQueryHandler(IProductService productService)
        {

            this._productService = productService;
        }
        public async Task<PaginatedResult<LowStockProductDto>> Handle(GetLowStockQuery request, CancellationToken cancellationToken)
        {



            return await _productService.GetLowStockProductsAsync(request);
        }
    }
}
