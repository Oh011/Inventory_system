using Application.Common.Interfaces;
using Project.Application.Common.Interfaces;

namespace InventorySystem.Extensions
{
    public static class WebApplicationExtensions
    {


        public static async Task<WebApplication> SeedDbAsync(this WebApplication application)
        {


            using var scope = application.Services.CreateScope();


            var dbSeeder = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbSeeder.InitializeAsync();

            var identitySeeder = scope.ServiceProvider.GetRequiredService<IdentityInitializer>();


            await identitySeeder.InitializeAsync();


            return application;
        }
    }
}
