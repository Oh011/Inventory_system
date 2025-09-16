using Domain.Entities;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Products.Dtos;
using InventorySystem.Application.Features.Products.Specifications;
using Shared.Results;

namespace InventorySystem.Application.Features.Products.queries.GetSalesProducts
{
    internal class GetSalesProductsLookupQueryHandler : IRequestHandler<GetSalesProductsLookupQuery, PaginatedResult<ProductSalesLookupDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUriService uriService;

        public GetSalesProductsLookupQueryHandler(IUnitOfWork unitOfWork, IUriService uriService)
        {
            _unitOfWork = unitOfWork;
            this.uriService = uriService;
        }

        public async Task<PaginatedResult<ProductSalesLookupDto>> Handle(GetSalesProductsLookupQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Product, int>();
            var specification = new SalesProductsSpecifications(request);






            var products = await repository.GetAllWithProjectionSpecifications(specification);
            var totalCount = await repository.CountAsync(specification.Criteria);


            foreach (var product in products)
            {

                product.ProductImageUrl = uriService.GetAbsoluteUri(product.ProductImageUrl);
            }

            return new PaginatedResult<ProductSalesLookupDto>(
                request.PageIndex,
                request.pageSize,
                totalCount,
                products
            );
        }
    }
}
