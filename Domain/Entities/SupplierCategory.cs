namespace Domain.Entities
{
    public class SupplierCategory
    {

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; } = null!;

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public bool IsPreferred { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
