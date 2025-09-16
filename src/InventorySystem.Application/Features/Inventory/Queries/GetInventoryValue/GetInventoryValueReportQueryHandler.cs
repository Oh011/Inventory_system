using Domain.Entities;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.Inventory.Specifications;

namespace InventorySystem.Application.Features.Inventory.Queries.GetInventoryValue
{
    internal class GetInventoryValueReportQueryHandler : IRequestHandler<GetInventoryValueReportQuery, InventoryValueReportResultDto>
    {


        private readonly IUnitOfWork unitOfWork;



        public GetInventoryValueReportQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<InventoryValueReportResultDto> Handle(GetInventoryValueReportQuery request, CancellationToken cancellationToken)
        {


            var repository = unitOfWork.GetRepository<Product, int>();

            var specifications = new InventoryValueReportSpecifications(request);


            var result = await repository.GetAllWithProjectionSpecifications(specifications);


            var inventoryResult = new InventoryValueReportResultDto()
            {

                Items = result.ToList(),
            };



            return inventoryResult;
        }
    }
}
