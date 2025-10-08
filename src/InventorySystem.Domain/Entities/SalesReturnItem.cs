using Domain.Entities;
using InventorySystem.Domain.Enums;

namespace InventorySystem.Domain.Entities
{
    public class SalesReturnItem
    {
        public int Id { get; set; }
        public int SalesReturnId { get; set; }
        public SalesReturn SalesReturn { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }


        public int SalesInvoiceItemId { get; set; }  // ✅ Link to the original invoice line
        public SalesInvoiceItem SalesInvoiceItem { get; set; } = null!;

        public int Quantity { get; set; }   // Quantity returned
        public decimal UnitPrice { get; set; } // Price per unit (snapshot)

        // New field
        public ReturnCondition Condition { get; set; } = ReturnCondition.Good;
    }

}
