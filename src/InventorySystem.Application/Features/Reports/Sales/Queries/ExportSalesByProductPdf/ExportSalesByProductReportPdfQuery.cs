using InventorySystem.Application.Features.Reports.Sales.FilterParameters;
using MediatR;

namespace InventorySystem.Application.Features.Reports.Sales.Queries.ExportPdf
{
    public class ExportSalesByProductReportPdfQuery : SalesByProductReportFilterParams, IRequest<byte[]>
    {
    }
}
