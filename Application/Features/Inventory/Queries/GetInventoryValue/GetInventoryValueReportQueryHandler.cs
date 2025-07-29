using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Features.Inventory.Dtos;
using Project.Application.Features.Inventory.Specifications;

namespace Project.Application.Features.Inventory.Queries.GetInventoryValue
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
