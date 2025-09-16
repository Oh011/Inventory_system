namespace InventorySystem.Application.Features.PurchaseOrders.Dtos
{
    public class PurchaseOrderDetailDto
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

        public int TotalQuantityReceived => OrderItems.Sum(i => i.QuantityReceived);
        public decimal TotalReceivedValue => OrderItems.Sum(i => i.UnitCost * i.QuantityReceived);


        public string RowVersion { get; set; } = string.Empty;




        public List<PurchaseOrderItemResultDto> OrderItems { get; set; } = new();


        public string ReceivedStatus
        {
            get
            {
                if (OrderItems.All(i => i.IsFullyReceived))
                    return "Fully Received";
                else if (OrderItems.Any(i => i.QuantityReceived > 0))
                    return "Partially Received";
                else
                    return "Pending";
            }
        }

    }
}
