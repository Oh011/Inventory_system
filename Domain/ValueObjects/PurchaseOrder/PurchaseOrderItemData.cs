namespace Domain.ValueObjects.PurchaseOrder
{


    public sealed class PurchaseOrderItemData
    {
        public int ProductId { get; }
        public int QuantityOrdered { get; }
        public decimal UnitCost { get; }

        public PurchaseOrderItemData(int productId, int quantityOrdered, decimal unitCost)
        {
            ProductId = productId;
            QuantityOrdered = quantityOrdered;
            UnitCost = unitCost;
        }
    }

}
