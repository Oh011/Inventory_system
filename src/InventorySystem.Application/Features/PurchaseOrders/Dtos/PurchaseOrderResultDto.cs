namespace Project.Application.Features.PurchaseOrders.Dtos
{
    public class PurchaseOrderResultDto
    {


        public int Id { get; set; }
        public string SupplierName { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public string Status { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal GrandTotal => TotalAmount + DeliveryFee;
        public string? Notes { get; set; }


        public string RowVersion { get; set; } = string.Empty;


        public decimal ReceivedValue => OrderItems.Sum(i => i.UnitCost * i.QuantityReceived);


        public List<PurchaseOrderItemResultDto> OrderItems { get; set; } = new();
    }
}
