using InventorySystem.Application.Features.Reports.Sales.FilterParameters;
using MediatR;

namespace InventorySystem.Application.Features.Reports.Sales.Queries.ExportSalesByCustomerPdf
{
    public class ExportSalesByCustomerReportPdfQuery : SalesByCustomerFilterParams, IRequest<byte[]>
    {
    }
}
