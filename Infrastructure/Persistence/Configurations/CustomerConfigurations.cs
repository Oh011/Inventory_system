using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Phone)
                .HasMaxLength(20);

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            // SalesInvoices: One customer has many invoices
            builder.HasMany(c => c.Invoices)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            // Global Query Filter for soft delete
            builder.HasQueryFilter(c => !c.IsDeleted);

            builder.HasIndex(c => c.FullName); //  Makes searches faster



            builder.HasQueryFilter(c => c.IsDeleted == false);
        }
    }
}
