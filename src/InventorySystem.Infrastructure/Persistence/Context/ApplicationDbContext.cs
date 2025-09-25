
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Product> Products { get; set; }



        public DbSet<Supplier> Suppliers { get; set; }


        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }



        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }



        public DbSet<SalesInvoice> SalesInvoices { get; set; }



        public DbSet<SalesInvoiceItem> SalesInvoicesItem { get; set; }



        public DbSet<Category> Category { get; set; }


        public DbSet<SupplierCategory> SuppliersCategory { get; set; }


        public DbSet<Employee> Employees { get; set; }


        public DbSet<Customer> Customers { get; set; }


        public DbSet<StockAdjustmentLog> stockAdjustmentLogs { get; set; }

    }
}
