using InventorySystem.Application.Features.Reports.Sales.FilterParameters;
using InventorySystem.Application.Features.Reports.Sales.Dtos;

namespace InventorySystem.Application.Features.Reports.Sales.Interfaces
{
    public interface ISalesReportService
    {
        Task<IEnumerable<SalesByProductReportDto>> GenerateReportAsync(SalesByProductReportFilterParams query, CancellationToken cancellationToken = default);
    }

}
