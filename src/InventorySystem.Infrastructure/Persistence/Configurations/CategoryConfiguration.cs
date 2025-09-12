
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);



            builder.HasMany(c => c.SupplierCategories)
    .WithOne(sc => sc.Category)
    .HasForeignKey(sc => sc.CategoryId)
    .OnDelete(DeleteBehavior.Cascade); // 🔥 Delete SupplierCategory row when Category is deleted




            builder.HasQueryFilter(c => c.IsDeleted == false);

        }

    }
}
