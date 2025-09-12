
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class SupplierCategoryConfiguration : IEntityTypeConfiguration<SupplierCategory>
    {
        public void Configure(EntityTypeBuilder<SupplierCategory> builder)
        {
            builder.ToTable("SupplierCategories");

            builder.HasKey(sc => new { sc.SupplierId, sc.CategoryId });

            builder.HasOne(sc => sc.Supplier)
                .WithMany(s => s.SupplierCategories)
                .HasForeignKey(sc => sc.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sc => sc.Category)
                .WithMany(c => c.SupplierCategories)
                .HasForeignKey(sc => sc.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);


            builder.Property(sc => sc.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }

}
