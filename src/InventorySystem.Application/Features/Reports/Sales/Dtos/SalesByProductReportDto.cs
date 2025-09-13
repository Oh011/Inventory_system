namespace Project.Application.Features.Reports.Sales.Dtos
{
    public class SalesByProductReportDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int UnitsSold { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal TotalRevenue => UnitsSold * SellingPrice;
        public string? CategoryName { get; set; }
    }

}
