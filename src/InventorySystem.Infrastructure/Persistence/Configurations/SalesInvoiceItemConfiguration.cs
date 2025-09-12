
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class SalesInvoiceItemConfiguration : IEntityTypeConfiguration<SalesInvoiceItem>
    {
        public void Configure(EntityTypeBuilder<SalesInvoiceItem> builder)
        {
            builder.ToTable("SalesInvoiceItems");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.UnitPrice)
                .HasPrecision(18, 2);

            builder.Property(i => i.Discount)
                .HasPrecision(18, 2);

            builder.HasOne(i => i.SalesInvoice)
                .WithMany(si => si.Items)
                .HasForeignKey(i => i.SalesInvoiceId);

            builder.HasOne(i => i.Product)
                .WithMany(p => p.SalesInvoices)
                .HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.Restrict);
            ;
        }
    }
}
