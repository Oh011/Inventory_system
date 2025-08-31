using Domain.Enums;

namespace Domain.Events.PurchaseOrder
{
    public class PurchaseOrderBaseEvent
    {

        public int PurchaseOrderId { get; }
        public string SupplierEmail { get; }
        public string SupplierName { get; }


        public PurchaseOrderStatus Status { get; }
        public int SupplierId { get; set; }

        public DateTime OccurredOn { get; } = DateTime.Now;

        public PurchaseOrderBaseEvent(int orderId, int supplierId, string supplierEmail, string supplierName, PurchaseOrderStatus status)
        {
            PurchaseOrderId = orderId;
            SupplierEmail = supplierEmail;
            SupplierName = supplierName;
            SupplierId = supplierId;
            Status = status;
        }
    }
}
