
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {



            builder
       .Property(p => p.RowVersion)
       .IsRowVersion().IsConcurrencyToken();

            builder.ToTable("PurchaseOrders");

            builder.HasKey(po => po.Id);

            builder.Property(po => po.Status)
                .HasConversion(po => po.ToString(), po => Enum.Parse<PurchaseOrderStatus>(po));


            builder.Property(po => po.TotalAmount)
                .HasPrecision(18, 2);

            builder.Property(po => po.DeliveryFee)
                .HasPrecision(18, 2);

            builder.Property(po => po.Notes)
                .HasMaxLength(1000);


            builder.Property(i => i.CreatedByEmployeeId)
    .IsRequired(false);

            builder.HasMany(po => po.Items)
    .WithOne(poi => poi.PurchaseOrder)
    .HasForeignKey(poi => poi.PurchaseOrderId)
    .OnDelete(DeleteBehavior.Cascade);



            builder.HasOne(po => po.Supplier)
                .WithMany(s => s.PurchaseOrders)
                .HasForeignKey(po => po.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
