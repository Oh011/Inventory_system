using MediatR;
using Project.Application.Features.Reports.Sales.Dtos;

namespace Project.Application.Features.Reports.Sales.Queries.Sales
{
    public class GetSalesReportQuery : SalesReportFilterParams, IRequest<IEnumerable<SalesReportDto>>
    {
        //public DateTime FromDate { get; set; } = DateTime.Today.AddDays(-10); // Default: 10 days ago
        //public DateTime ToDate { get; set; } = DateTime.Today;                // Default: Today

        //public int? ProductId { get; set; }

        //public int? TopCount { get; set; } = 10;


        //public SalesReportSortOptions? ReportSortOptions { get; set; }
        //public int? CategoryId { get; set; }
    }

}
