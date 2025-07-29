using MediatR;
using Project.Application.Features.Inventory.Filters;

namespace Project.Application.Features.Inventory.Queries.ExportAdjustmentLogsPdf
{
    public class ExportAdjustmentLogsPdfQuery : AdjustmentLogsFilter, IRequest<byte[]>
    {

    }
}
