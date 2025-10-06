using Domain.Common;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class PurchaseOrderItem : BaseEntity
    {



        // Foreign key to PurchaseOrder
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; } = null!;

        // Foreign key to Product
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int QuantityOrdered { get; set; }

        public int QuantityReceived { get; set; } = 0;

        public decimal UnitCost { get; set; }

        // Optional: Stored in DB or computed
        public decimal Subtotal => QuantityOrdered * UnitCost;



        public void UpdateQuantityReceived(int quantity)
        {

            var newTotal = this.QuantityReceived + quantity;

            if (newTotal > QuantityOrdered)
                throw new DomainException($"Product {ProductId} received quantity {newTotal}, but only {QuantityOrdered} was ordered.");

            this.QuantityReceived = newTotal;

        }
    }
}
