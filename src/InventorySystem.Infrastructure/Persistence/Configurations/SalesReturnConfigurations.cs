using InventorySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.Persistence.Configurations
{
    internal class SalesReturnConfigurations : IEntityTypeConfiguration<SalesReturn>
    {
        public void Configure(EntityTypeBuilder<SalesReturn> builder)
        {


            builder.ToTable("SalesReturns");

            builder.HasKey(sr => sr.Id);

            // Relationship: SalesReturn → SalesInvoice (many-to-one)
            builder.HasOne(sr => sr.SalesInvoice)
                .WithMany(si => si.SalesReturns)
                .HasForeignKey(sr => sr.SalesInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: SalesReturn → SalesReturnItems (one-to-many)
            builder.HasMany(sr => sr.Items)
                .WithOne(sri => sri.SalesReturn)
                .HasForeignKey(sri => sri.SalesReturnId)
                .OnDelete(DeleteBehavior.Cascade);




            builder.Property(sr => sr.Reason)
                .HasMaxLength(500);

            builder.Property(sr => sr.TotalRefundAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(sr => sr.ReturnDate)
                .IsRequired();
        }
    }
}
