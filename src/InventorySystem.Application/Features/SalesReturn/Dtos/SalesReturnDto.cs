namespace InventorySystem.Application.Features.SalesReturn.Dtos
{
    public class SalesReturnDto
    {
        public int Id { get; set; }
        public int SalesInvoiceId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Reason { get; set; } = string.Empty;

        public decimal TotalRefundAmount { get; set; }
        public List<SalesReturnItemDto> Items { get; set; } = new();
    }

}
