using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class StockAdjustmentLogConfigurations : IEntityTypeConfiguration<StockAdjustmentLog>
    {
        public void Configure(EntityTypeBuilder<StockAdjustmentLog> builder)
        {
            builder.ToTable("StockAdjustmentLogs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.QuantityChange)
                   .IsRequired();

            builder.Property(x => x.Reason)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(x => x.AdjustedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(x => x.Product)
                   .WithMany()
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.AdjustedBy)
                   .WithMany()
                   .HasForeignKey(x => x.AdjustedById)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
