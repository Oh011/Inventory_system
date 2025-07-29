
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Email)
                .HasMaxLength(100);

            builder.Property(s => s.Phone)
                .HasMaxLength(20);

            builder.Property(s => s.Address)
                .HasMaxLength(250);

            builder.Property(s => s.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");



            builder.HasMany(s => s.SupplierCategories)
          .WithOne(sc => sc.Supplier)
          .HasForeignKey(sc => sc.SupplierId)
          .OnDelete(DeleteBehavior.Cascade); // 🔥 Delete SupplierCategory row when Supplier is deleted





            builder.HasQueryFilter(c => c.IsDeleted == false);


        }
    }
}
