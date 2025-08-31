using Domain.Common;
using Domain.Enums;
using Domain.Events.PurchaseOrder;
using Domain.Exceptions;
using Domain.ValueObjects.PurchaseOrder;

namespace Domain.Entities
{
    public class PurchaseOrder : BaseEntity
    {



        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public DateTime? ExpectedDate { get; set; }

        public PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.Pending;

        public decimal TotalAmount { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal GrandTotal => TotalAmount + DeliveryFee; // Computed in app, not stored

        public int? CreatedByEmployeeId { get; set; }

        public Employee CreatedByEmployee { get; set; }


        public string? Notes { get; set; }

        public ICollection<PurchaseOrderItem>? Items { get; set; } = new List<PurchaseOrderItem>();



        public byte[] RowVersion { get; set; } = null!;



        public bool CanTransitionTo(PurchaseOrderStatus newStatus)
        {
            return this.Status switch
            {
                PurchaseOrderStatus.Pending => newStatus is PurchaseOrderStatus.Cancelled or PurchaseOrderStatus.PartiallyReceived or PurchaseOrderStatus.Received,
                PurchaseOrderStatus.PartiallyReceived => newStatus == PurchaseOrderStatus.Received,
                _ => false
            };
        }


        public void UpdateStatusBasedOnReceivedQuantities()
        {
            int totalOrdered = Items.Sum(i => i.QuantityOrdered);
            int totalReceived = Items.Sum(i => i.QuantityReceived);

            if (totalReceived < totalOrdered)
                Status = PurchaseOrderStatus.PartiallyReceived;
            else if (totalReceived == totalOrdered)
                Status = PurchaseOrderStatus.Received;
            else
                throw new DomainException("Received more than ordered"); // rule
        }

        public void MarkAsCreated(int supplierId, string supplierName, string supplierEmail, PurchaseOrderStatus status)
        {
            var domainEvent = new PurchaseOrderCreatedDomainEvent(this.Id, supplierId,
                 supplierEmail, supplierName, status);


            var statusChangedEvent = new PurchaseOrderStatusChangedDomainEvent(this.Id, supplierId, supplierEmail, supplierName, status);

            AddDomainEvent(domainEvent);
            AddDomainEvent(statusChangedEvent);
        }


        public void MarkAsCanceled(int supplierId, string supplierName, string supplierEmail, PurchaseOrderStatus status)
        {
            var domainEvent = new PurchaseOrderCanceledDomainEvent(this.Id, supplierId,
                 supplierEmail, supplierName, status);

            var statusChangedEvent = new PurchaseOrderStatusChangedDomainEvent(this.Id, supplierId, supplierEmail, supplierName, status);

            AddDomainEvent(domainEvent);
            AddDomainEvent(statusChangedEvent);

        }



        public void MarkAsReceived(int supplierId, string supplierName, string supplierEmail, PurchaseOrderStatus status, Dictionary<int, int> ReceivedItems)
        {

            var domainEvent = new PurchaseOrderReceivedDomainEvent(this.Id, supplierId
                , supplierEmail, supplierName,
            status, ReceivedItems);

            var statusChangedEvent = new PurchaseOrderStatusChangedDomainEvent(this.Id, supplierId, supplierEmail, supplierName, status);

            AddDomainEvent(domainEvent);
            AddDomainEvent(statusChangedEvent);

        }



        public decimal AddItems(IEnumerable<PurchaseOrderItemData> items)
        {
            decimal total = 0;

            foreach (var item in items)
            {
                Items.Add(new PurchaseOrderItem
                {
                    ProductId = item.ProductId,
                    QuantityOrdered = item.QuantityOrdered,
                    QuantityReceived = 0,
                    UnitCost = item.UnitCost
                });

                total += item.QuantityOrdered * item.UnitCost;
            }

            this.TotalAmount = total;

            return total;
        }


    }
}


//Domain Exception :


//✅ Why 400 Bad Request?
//A DomainException means:

//❌ The user's input or action violated a business rule.

//That’s exactly what 400 Bad Request means in HTTP:

//🔁 “The server cannot process the request because of a client-side issue.”