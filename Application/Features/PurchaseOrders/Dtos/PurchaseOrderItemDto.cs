namespace Project.Application.Features.PurchaseOrders.Dtos
{
    public class PurchaseOrderItemDto
    {

        public int ProductId { get; set; }
        public int QuantityOrdered { get; set; }
        public decimal UnitCost { get; set; }
    }
}
