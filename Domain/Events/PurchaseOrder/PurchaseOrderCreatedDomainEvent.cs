using Domain.Common;
using Domain.Enums;

namespace Domain.Events.PurchaseOrder
{
    public class PurchaseOrderCreatedDomainEvent : PurchaseOrderBaseEvent, IDomainEvent
    {



        public PurchaseOrderCreatedDomainEvent(int purchaseOrderId, int supplierId, string supplierEmail, string supplierName, PurchaseOrderStatus status
          ) : base(purchaseOrderId, supplierId, supplierName, supplierEmail, status)
        {



        }





    }
}
