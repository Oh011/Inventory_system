using Domain.Common;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {




        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property (optional, but very useful)

        public ICollection<Product>? Products { get; set; }


        public ICollection<SupplierCategory>? SupplierCategories { get; set; }
    }
}
