using Project.Application.Features.Reports.Sales.Dtos;

namespace Project.Application.Features.Reports.Sales.Interfaces
{
    public interface ISalesReportService
    {
        Task<IEnumerable<SalesReportDto>> GenerateReportAsync(SalesReportFilterParams query, CancellationToken cancellationToken = default);
    }

}
