using InventorySystem.Application.Features.Reports.Sales.FilterParameters;
using Project.Application.Features.Reports.Sales.Dtos;

namespace Project.Application.Features.Reports.Sales.Interfaces
{
    public interface ISalesReportService
    {
        Task<IEnumerable<SalesByProductReportDto>> GenerateReportAsync(SalesByProductReportFilterParams query, CancellationToken cancellationToken = default);
    }

}
