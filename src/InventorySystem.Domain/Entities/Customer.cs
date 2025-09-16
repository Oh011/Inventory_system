using Domain.Common;

namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }


        public bool IsActive { get; set; } = true;


        public ICollection<SalesInvoice> Invoices { get; set; } = new List<SalesInvoice>();
    }
}
