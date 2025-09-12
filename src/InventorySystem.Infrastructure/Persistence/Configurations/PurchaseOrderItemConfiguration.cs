
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PurchaseOrderItemConfiguration : IEntityTypeConfiguration<PurchaseOrderItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
        {
            builder.ToTable("PurchaseOrderItems");

            builder.HasKey(poi => poi.Id);

            builder.Property(poi => poi.UnitCost)
                .HasPrecision(18, 2);

            builder.HasOne(poi => poi.Product)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(poi => poi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(poi => poi.PurchaseOrder)
                .WithMany(po => po.Items)
                .HasForeignKey(poi => poi.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
