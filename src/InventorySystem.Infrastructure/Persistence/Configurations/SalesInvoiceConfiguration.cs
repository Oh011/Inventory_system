
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class SalesInvoiceConfiguration : IEntityTypeConfiguration<SalesInvoice>
    {
        public void Configure(EntityTypeBuilder<SalesInvoice> builder)
        {
            builder.ToTable("SalesInvoices");

            builder.HasKey(i => i.Id);



            builder.Property(i => i.CreatedByEmployeeId)
                .IsRequired(false);

            builder.Property(i => i.TotalAmount)
                .HasPrecision(18, 2);

            builder.Property(i => i.InvoiceDiscount)
                .HasPrecision(18, 2);

            builder.Property(i => i.DeliveryFee)
                .HasPrecision(18, 2);

            builder.Property(i => i.PaymentMethod)
                .HasConversion(i => i.ToString(), i => Enum.Parse<PaymentMethod>(i));


            builder.HasMany(i => i.Items)
                .WithOne(Si => Si.SalesInvoice)
                .HasForeignKey(Si => Si.SalesInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Property(i => i.Notes)
                .HasMaxLength(1000);
        }
    }
}
