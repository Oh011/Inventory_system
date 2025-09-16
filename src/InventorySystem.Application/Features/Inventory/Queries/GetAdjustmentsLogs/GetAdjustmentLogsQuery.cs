using MediatR;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.Inventory.Filters;
using Shared.Parameters;
using Shared.Results;

namespace InventorySystem.Application.Features.Inventory.Queries.GetAdjustmentsLogs
{

    public class GetAdjustmentLogsQuery : AdjustmentLogsFilter, IRequest<PaginatedResult<StockAdjustmentLogDto>>
    {

        public PaginationQueryParameters Pagination { get; set; } = new();


    }
}
