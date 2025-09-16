using Domain.Entities;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.Inventory.Specifications;
using Shared.Results;

namespace InventorySystem.Application.Features.Inventory.Queries.GetAdjustmentsLogs
{
    internal class GetAdjustmentLogsQueryHandler : IRequestHandler<GetAdjustmentLogsQuery, PaginatedResult<StockAdjustmentLogDto>>
    {


        private readonly IUnitOfWork unitOfWork;


        public GetAdjustmentLogsQueryHandler(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }



        public async Task<PaginatedResult<StockAdjustmentLogDto>> Handle(GetAdjustmentLogsQuery request, CancellationToken cancellationToken)
        {

            var stockAdjustmentRepository = unitOfWork.GetRepository<StockAdjustmentLog, int>();


            var specifications = new GetAdjustmentLogsSpecifications(request, request.Pagination.PageIndex, request.Pagination.pageSize);



            var result = await stockAdjustmentRepository.GetAllWithProjectionSpecifications(specifications);
            var totalCount = await stockAdjustmentRepository.CountAsync(specifications.Criteria);



            return new PaginatedResult<StockAdjustmentLogDto>(

               request.Pagination.PageIndex, request.Pagination.pageSize, totalCount, result

                );
        }
    }
}
