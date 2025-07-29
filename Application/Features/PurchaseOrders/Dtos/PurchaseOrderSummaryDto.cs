namespace Project.Application.Features.PurchaseOrders.Dtos
{
    public class PurchaseOrderSummaryDto
    {
        public int Id { get; set; }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public decimal GrandTotal { get; set; }
    }

}
