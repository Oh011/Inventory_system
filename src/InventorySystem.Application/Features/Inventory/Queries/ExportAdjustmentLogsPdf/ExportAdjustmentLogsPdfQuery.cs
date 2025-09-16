using MediatR;
using InventorySystem.Application.Features.Inventory.Filters;

namespace InventorySystem.Application.Features.Inventory.Queries.ExportAdjustmentLogsPdf
{
    public class ExportAdjustmentLogsPdfQuery : AdjustmentLogsFilter, IRequest<byte[]>
    {

    }
}
