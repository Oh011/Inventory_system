using AutoMapper;
using Domain.Entities;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Products.Dtos;
using InventorySystem.Application.Features.Products.queries.GetProductsForSupplier;
using InventorySystem.Application.Features.Products.Specifications;
using Shared.Results;

namespace InventorySystem.Application.Features.Products.queries.GetProductBySupplier
{
    internal class GetPurchaseProductsLookupQueryHandler : IRequestHandler<GetPurchaseProductsLookupQuery, PaginatedResult<ProductPurchaseOrderLookUpDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPurchaseProductsLookupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ProductPurchaseOrderLookUpDto>> Handle(GetPurchaseProductsLookupQuery request, CancellationToken cancellationToken)
        {

            var repository = _unitOfWork.GetRepository<Product, int>();
            var specifications = new SupplierProductsSpecifications(request);

            var products = await repository.GetAllWithProjectionSpecifications(specifications);
            var totalCount = await repository.CountAsync(specifications.Criteria);



            return new PaginatedResult<ProductPurchaseOrderLookUpDto>(request.PageIndex, request.pageSize, totalCount
                , products);
        }
    }
}
