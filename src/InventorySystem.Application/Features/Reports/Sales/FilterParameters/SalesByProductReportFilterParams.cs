using InventorySystem.Application.Features.Reports.Sales.SortOptions;

namespace InventorySystem.Application.Features.Reports.Sales.FilterParameters
{
    public class SalesByProductReportFilterParams
    {
        public DateTime FromDate { get; set; } = DateTime.Today.AddDays(-10);
        public DateTime ToDate { get; set; } = DateTime.Today;
        public int? ProductId { get; set; }
        public int? TopCount { get; set; } = 10;
        public SalesByProductReportSortOptions? ReportSortOptions { get; set; }
        public int? CategoryId { get; set; }
    }

}
