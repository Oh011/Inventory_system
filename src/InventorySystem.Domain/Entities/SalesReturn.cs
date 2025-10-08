using Domain.Common;
using Domain.Entities;

namespace InventorySystem.Domain.Entities
{
    public class SalesReturn : BaseEntity
    {
        public int Id { get; set; }

        public int SalesInvoiceId { get; set; } // Reference to original invoice
        public SalesInvoice SalesInvoice { get; set; }

        public DateTime ReturnDate { get; set; } = DateTime.UtcNow;
        public string Reason { get; set; } = string.Empty;

        public ICollection<SalesReturnItem> Items { get; set; } = new List<SalesReturnItem>();


        public decimal TotalRefundAmount { get; set; }

    }
}
