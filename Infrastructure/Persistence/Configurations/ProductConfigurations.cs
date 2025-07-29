
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);


            builder.HasIndex(p => p.Name);


            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Barcode)
                .HasMaxLength(50);

            builder.HasIndex(p => p.Barcode)
                .IsUnique()
                .HasFilter("[Barcode] IS NOT NULL");

            builder.Property(p => p.Unit)
                .HasConversion(p => p.ToString(), p => Enum.Parse<UnitType>(p));


            builder.Property(p => p.CostPrice)
                .HasPrecision(18, 2);

            builder.Property(p => p.SellingPrice)
                .HasPrecision(18, 2);

            builder.Property(p => p.ProductImageUrl)
                .HasMaxLength(255);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");



            builder.HasOne(p => p.Category).WithMany(p => p.Products).HasForeignKey(p => p.CategoryId)
                 .OnDelete(DeleteBehavior.SetNull);




            builder.HasQueryFilter(c => c.IsDeleted == false);
        }
    }
}
