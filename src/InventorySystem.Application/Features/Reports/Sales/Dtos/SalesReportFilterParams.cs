using Project.Application.Common.Enums.SortOptions;

namespace Project.Application.Features.Reports.Sales.Dtos
{
    public class SalesReportFilterParams
    {
        public DateTime FromDate { get; set; } = DateTime.Today.AddDays(-10);
        public DateTime ToDate { get; set; } = DateTime.Today;
        public int? ProductId { get; set; }
        public int? TopCount { get; set; } = 10;
        public SalesReportSortOptions? ReportSortOptions { get; set; }
        public int? CategoryId { get; set; }
    }

}
