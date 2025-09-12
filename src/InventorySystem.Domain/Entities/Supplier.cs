using Domain.Common;

namespace Domain.Entities
{
    public class Supplier : BaseEntity
    {



        public string Name { get; set; } = null!;

        public string? ContactName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property to many-to-many join entity
        public ICollection<SupplierCategory>? SupplierCategories { get; set; }


        public bool IsDeleted { get; set; } = false;



        public ICollection<PurchaseOrder>? PurchaseOrders { get; set; }




        public void SyncCategories(IEnumerable<int> categoryIds)
        {


            var toRemove = SupplierCategories
              .Where(sc => !categoryIds.Contains(sc.CategoryId))
              .ToList();

            foreach (var item in toRemove)
                SupplierCategories.Remove(item);

            var existingCategoryIds = SupplierCategories
                .Select(sc => sc.CategoryId)
                .ToHashSet();

            var toAdd = categoryIds
                .Where(id => !existingCategoryIds.Contains(id));

            foreach (var categoryId in toAdd)
            {
                SupplierCategories.Add(new SupplierCategory
                {
                    CategoryId = categoryId,
                    SupplierId = Id
                });
            }

        }
    }
}
