using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using MediatR;
using InventorySystem.Application.Features.Products.Dtos;
using InventorySystem.Application.Features.Products.Intrefaces;
using Shared.Results;

namespace InventorySystem.Application.Features.Products.queries.GetProducts
{
    internal class GetProductQueryHandler : IRequestHandler<GetProductsQuery, PaginatedResult<ProductResultDto>>
    {


        private readonly IProductService _productService;
        private readonly IUriService uriService;

        public GetProductQueryHandler(IProductService productService, IUriService uriService)
        {

            this.uriService = uriService;
            _productService = productService;
        }
        public async Task<PaginatedResult<ProductResultDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllProducts(request);


            foreach (var product in result.Items)
            {

                product.ProductImageUrl = uriService.GetAbsoluteUri(product.ProductImageUrl);
            }


            return result;
        }
    }
}
