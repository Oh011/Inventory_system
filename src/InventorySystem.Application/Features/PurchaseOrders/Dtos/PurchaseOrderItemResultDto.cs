namespace InventorySystem.Application.Features.PurchaseOrders.Dtos
{
    public class PurchaseOrderItemResultDto
    {


        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ProductCode { get; set; }
        public decimal UnitCost { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public decimal Subtotal => UnitCost * QuantityOrdered;


        public int QuantityRemaining => QuantityOrdered - QuantityReceived;

        public bool IsFullyReceived => QuantityRemaining == 0;
    }
}
