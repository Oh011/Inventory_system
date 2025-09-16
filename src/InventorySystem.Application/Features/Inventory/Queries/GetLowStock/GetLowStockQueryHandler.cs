using MediatR;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.Products.Intrefaces;
using Shared.Results;

namespace InventorySystem.Application.Features.Inventory.Queries.GetLowStock
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
