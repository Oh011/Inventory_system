using Domain.Enums;

namespace Domain.Events.PurchaseOrder
{
    public class PurchaseOrderBaseEvent
    {

        public int PurchaseOrderId { get; }
        public string SupplierEmail { get; }
        public string SupplerName { get; }


        public PurchaseOrderStatus Status { get; }
        public int SupplierId { get; set; }

        public DateTime OccurredOn { get; } = DateTime.Now;

        public PurchaseOrderBaseEvent(int orderId, int supplierId, string supplierEmail, string supplerName, PurchaseOrderStatus status)
        {
            PurchaseOrderId = orderId;
            SupplierEmail = supplierEmail;
            SupplerName = supplerName;
            SupplierId = supplierId;
            Status = status;
        }
    }
}
