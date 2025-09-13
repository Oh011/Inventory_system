using InventorySystem.Application.Features.Reports.Sales.Dtos;
using Project.Application.Features.Reports.Sales.Dtos;

namespace Project.Application.Common.Interfaces.PdfGenerators
{
    public interface ISalesReportPdfGenerator
    {


        public byte[] GenerateSalesByCustomerReportPdf(
IEnumerable<SalesByCustomerReportDto> data,
DateTime fromDate,
DateTime toDate);

        public byte[] GenerateSalesByCategoryReportPdf(
    IEnumerable<SalesByCategoryReportDto> data,
    DateTime fromDate,
    DateTime toDate);
        byte[] GenerateSalesByProductReportPdf(IEnumerable<SalesByProductReportDto> data, DateTime fromDate, DateTime toDate);
    }

}
