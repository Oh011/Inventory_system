namespace InventorySystem.Infrastructure.Persistence.Context
{
    using global::Infrastructure.Persistence.Context;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;




    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();



            var connectionString = "Server=.;DataBase=InventorySystem;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True";

            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }

}
