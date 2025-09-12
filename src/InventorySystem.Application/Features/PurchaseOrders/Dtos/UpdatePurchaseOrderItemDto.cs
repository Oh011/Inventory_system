namespace Project.Application.Features.PurchaseOrders.Dtos
{
    public class UpdatePurchaseOrderItemDto
    {

        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public int QuantityReceived { get; set; }
    }
}
