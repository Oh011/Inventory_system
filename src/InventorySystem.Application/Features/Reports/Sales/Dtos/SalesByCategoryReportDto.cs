namespace InventorySystem.Application.Features.Reports.Sales.Dtos
{
    public class SalesByCategoryReportDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
        public int UnitsSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }

}
