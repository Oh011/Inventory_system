using MediatR;
using Project.Application.Features.Reports.Sales.Dtos;

namespace Project.Application.Features.Reports.Sales.Queries.ExportPdf
{
    public class ExportSalesReportPdfQuery : SalesReportFilterParams, IRequest<byte[]>
    {
    }
}
