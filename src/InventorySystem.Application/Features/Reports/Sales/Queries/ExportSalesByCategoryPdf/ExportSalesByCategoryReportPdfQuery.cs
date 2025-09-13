using InventorySystem.Application.Features.Reports.Sales.FilterParameters;
using MediatR;

namespace InventorySystem.Application.Features.Reports.Sales.Queries.ExportSalesByCategoryPdf
{
    public class ExportSalesByCategoryReportPdfQuery : SalesByCategoryFilterParams, IRequest<byte[]>
    {
    }
}
