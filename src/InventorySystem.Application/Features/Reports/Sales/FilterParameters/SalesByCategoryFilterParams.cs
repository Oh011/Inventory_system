using InventorySystem.Application.Features.Reports.Sales.SortOptions;

namespace InventorySystem.Application.Features.Reports.Sales.FilterParameters
{
    public class SalesByCategoryFilterParams
    {
        public DateTime FromDate { get; set; } = DateTime.Today.AddDays(-10);
        public DateTime ToDate { get; set; } = DateTime.Today;
        public int? TopCount { get; set; } = 10;
        public SalesByCategoryReportSortOptions? ReportSortOptions { get; set; }
    }

}
