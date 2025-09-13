using InventorySystem.Application.Features.Reports.Sales.SortOptions;

namespace InventorySystem.Application.Features.Reports.Sales.FilterParameters
{
    public class SalesByCustomerFilterParams
    {
        public DateTime FromDate { get; set; } = DateTime.Today.AddDays(-30);
        public DateTime ToDate { get; set; } = DateTime.Today;

        public int? CustomerId { get; set; }
        public string? Search { get; set; } // Name, Email, or Phone
        public int? TopCount { get; set; } = 10;
        public SalesByCustomerSortOptions? SortOption { get; set; }
    }

}
