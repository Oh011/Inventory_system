using InventorySystem.Domain.Entities;
using InventorySystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.Persistence.Configurations
{
    internal class SalesReturnItemConfiguration : IEntityTypeConfiguration<SalesReturnItem>
    {
        public void Configure(EntityTypeBuilder<SalesReturnItem> builder)
        {


            builder.ToTable("SalesReturnItems");

            builder.HasKey(sri => sri.Id);

            // Relationship: SalesReturnItem → SalesReturn
            builder.HasOne(sri => sri.SalesReturn)
                .WithMany(sr => sr.Items)
                .HasForeignKey(sri => sri.SalesReturnId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: SalesReturnItem → Product
            builder.HasOne(sri => sri.Product)
                .WithMany()
                .HasForeignKey(sri => sri.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship: SalesReturnItem → SalesInvoiceItem
            builder.HasOne(sri => sri.SalesInvoiceItem)
                .WithMany(sii => sii.SalesReturnItems)
                .HasForeignKey(sri => sri.SalesInvoiceItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(sri => sri.UnitPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(sri => sri.Quantity)
                .IsRequired();


            builder.Property(sr => sr.Condition).HasConversion(c => c.ToString(),
                c => Enum.Parse<ReturnCondition>(c));

            builder.Property(sri => sri.Condition)
                .IsRequired();
        }
    }
}
