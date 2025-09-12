using Domain.Common;
using Domain.Enums;

namespace Domain.Events.PurchaseOrder
{
    public class PurchaseOrderReceivedDomainEvent : PurchaseOrderBaseEvent, IDomainEvent
    {


        public Dictionary<int, int> ReceivedItems { get; }

        public DateTime OccurredOn { get; } = DateTime.Now;

        public PurchaseOrderReceivedDomainEvent(int purchaseOrderId, int supplierId, string supplierEmail, string supplierName, PurchaseOrderStatus status, Dictionary<int, int> items)

            : base(purchaseOrderId, supplierId, supplierEmail, supplierName, status)
        {


            ReceivedItems = items;
        }
    }

}
