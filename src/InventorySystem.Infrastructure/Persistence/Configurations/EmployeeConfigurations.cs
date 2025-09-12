
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{

    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Table name (optional)
            builder.ToTable("Employees");

            // Primary key
            builder.HasKey(e => e.Id);

            // Required fields
            builder.Property(e => e.ApplicationUserId)
                .IsRequired();

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Optional fields with max lengths
            builder.Property(e => e.JobTitle)
                .HasMaxLength(100);

            builder.Property(e => e.ProfileImageUrl)
                .HasMaxLength(255);

            builder.Property(e => e.Address)
                .HasMaxLength(255);

            builder.Property(e => e.NationalId)
                .HasMaxLength(20);

            // Relationships (optional setup, only if you have PurchaseOrder/SalesInvoice classes)
            builder.HasMany(e => e.CreatedPurchaseOrders)
                .WithOne(p => p.CreatedByEmployee) // or WithOne(po => po.Employee)
                .HasForeignKey(p => p.CreatedByEmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.CreatedSalesInvoices)
                .WithOne(p => p.CreatedByEmployee) // or WithOne(si => si.Employee)
                .HasForeignKey(p => p.CreatedByEmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
