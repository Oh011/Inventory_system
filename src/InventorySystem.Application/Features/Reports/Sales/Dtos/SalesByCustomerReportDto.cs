namespace InventorySystem.Application.Features.Reports.Sales.Dtos
{
    public class SalesByCustomerReportDto
    {
        public int CustomerId { get; set; }       // optional, internal use
        public string CustomerName { get; set; } = default!;
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public int NumberOfInvoices { get; set; }
        public int TotalUnitsPurchased { get; set; }  // sum of SalesInvoiceItem.Quantity
        public decimal TotalRevenue { get; set; }     // sum of FinalTotal across invoices
    }

}
