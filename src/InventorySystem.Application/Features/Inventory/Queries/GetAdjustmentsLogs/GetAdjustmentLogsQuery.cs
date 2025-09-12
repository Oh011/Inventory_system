using MediatR;
using Project.Application.Features.Inventory.Dtos;
using Project.Application.Features.Inventory.Filters;
using Shared.Parameters;
using Shared.Results;

namespace Project.Application.Features.Inventory.Queries.GetAdjustmentsLogs
{

    public class GetAdjustmentLogsQuery : AdjustmentLogsFilter, IRequest<PaginatedResult<StockAdjustmentLogDto>>
    {

        public PaginationQueryParameters Pagination { get; set; } = new();


    }
}
