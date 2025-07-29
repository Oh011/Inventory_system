using Domain.Common;


namespace Domain.Entities
{
    public class Employee : BaseEntity
    {


        public string ApplicationUserId { get; set; } // Foreign Key to Identity User

        public string FullName { get; set; }
        public string Role { get; set; } // e.g., "Admin", "Salesperson", etc.
        public string? JobTitle { get; set; }


        public string? ProfileImageUrl { get; set; } // Optional profile photo

        public string? Address { get; set; }
        public string? NationalId { get; set; }

        public bool IsActive { get; set; } = true;


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation for business logic
        // Navigation properties — initialize with empty HashSet
        public ICollection<PurchaseOrder> CreatedPurchaseOrders { get; set; } = new HashSet<PurchaseOrder>();
        public ICollection<SalesInvoice> CreatedSalesInvoices { get; set; } = new HashSet<SalesInvoice>();




    }
}

