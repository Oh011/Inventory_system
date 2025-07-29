using Project.Application.Features.Reports.Sales.Dtos;

namespace Project.Application.Common.Interfaces.PdfGenerators
{
    public interface ISalesReportPdfGenerator
    {
        byte[] GenerateSalesReportPdf(IEnumerable<SalesReportDto> data, DateTime fromDate, DateTime toDate);
    }

}
