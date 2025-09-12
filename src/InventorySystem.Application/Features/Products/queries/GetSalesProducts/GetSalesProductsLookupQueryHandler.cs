using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Features.Products.Dtos;
using Project.Application.Features.Products.Specifications;
using Shared.Results;

namespace Project.Application.Features.Products.queries.GetSalesProducts
{
    internal class GetSalesProductsLookupQueryHandler : IRequestHandler<GetSalesProductsLookupQuery, PaginatedResult<SalesProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUriService uriService;

        public GetSalesProductsLookupQueryHandler(IUnitOfWork unitOfWork, IUriService uriService)
        {
            _unitOfWork = unitOfWork;
            this.uriService = uriService;
        }

        public async Task<PaginatedResult<SalesProductDto>> Handle(GetSalesProductsLookupQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Product, int>();
            var specification = new SalesProductsSpecifications(request);

            var products = await repository.GetAllWithProjectionSpecifications(specification);
            var totalCount = await repository.CountAsync(specification.Criteria);


            foreach (var product in products)
            {

                product.ProductImageUrl = uriService.GetAbsoluteUri(product.ProductImageUrl);
            }

            return new PaginatedResult<SalesProductDto>(
                request.PageIndex,
                request.pageSize,
                totalCount,
                products
            );
        }
    }
}
