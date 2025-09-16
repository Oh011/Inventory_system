using Domain.Entities;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.Inventory.Specifications;
using Shared.Results;

namespace InventorySystem.Application.Features.Inventory.Queries.GetInventory
{
    internal class GetInventoryQueryHandler : IRequestHandler<GetInventoryQuery, PaginatedResult<InventoryDto>>
    {


        private readonly IUnitOfWork _unitOfWork;


        public GetInventoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<InventoryDto>> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
        {

            var repository = _unitOfWork.GetRepository<Product, int>();

            var specifications = new InventoryProductSpecifications(request);


            var result = await repository.GetAllWithProjectionSpecifications(specifications);
            var totalCount = await repository.CountAsync(specifications.Criteria);



            return new PaginatedResult<InventoryDto>(request.PageIndex, request.pageSize
                , totalCount, result
        );
        }
    }
}
