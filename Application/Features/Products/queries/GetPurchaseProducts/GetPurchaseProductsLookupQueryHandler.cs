using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Features.Products.Dtos;
using Project.Application.Features.Products.queries.GetProductsForSupplier;
using Project.Application.Features.Products.Specifications;
using Shared.Results;

namespace Project.Application.Features.Products.queries.GetProductBySupplier
{
    internal class GetPurchaseProductsLookupQueryHandler : IRequestHandler<GetPurchaseProductsLookupQuery, PaginatedResult<PurchaseProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPurchaseProductsLookupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<PurchaseProductDto>> Handle(GetPurchaseProductsLookupQuery request, CancellationToken cancellationToken)
        {

            var repository = _unitOfWork.GetRepository<Product, int>();
            var specifications = new SupplierProductsSpecifications(request);

            var products = await repository.GetAllWithProjectionSpecifications(specifications);
            var totalCount = await repository.CountAsync(specifications.Criteria);



            return new PaginatedResult<PurchaseProductDto>(request.PageIndex, request.pageSize, totalCount
                , products);
        }
    }
}
