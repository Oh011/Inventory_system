namespace InventorySystem.Application.Features.SalesReturn.Dtos
{
    public class SalesReturnSummaryDto
    {
        public int Id { get; set; }
        public int SalesInvoiceId { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalRefundAmount { get; set; }
        public string Reason { get; set; } = string.Empty;
    }

}
