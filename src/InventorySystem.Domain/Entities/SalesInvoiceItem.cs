using Domain.Common;
using InventorySystem.Domain.Entities;

namespace Domain.Entities
{
    public class SalesInvoiceItem : BaseEntity
    {

        public int Id { get; set; }

        public int SalesInvoiceId { get; set; }
        public SalesInvoice SalesInvoice { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;


        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; } // Per-line item discount

        public decimal Subtotal => (UnitPrice * Quantity) - Discount;


        public ICollection<SalesReturnItem> SalesReturnItems { get; set; } = new List<SalesReturnItem>();
    }
}
