using Application.Common.Interfaces;
using Hangfire;
using Infrastructure.Services.Jobs;
using InventorySystem.Application.Common.Interfaces;

namespace InventorySystem.Extensions
{
    public static class WebApplicationExtensions
    {

        public static async Task<WebApplication> StartHangFire(this WebApplication application)
        {


            using (var scope = application.Services.CreateScope())
            {
                var recurringJobs = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

                recurringJobs.AddOrUpdate<DailyStockCheckJob>(
                    "DailyStockCheck",
                    job => job.RunAsync(),
                    Cron.Hourly()
                );
            }

            return application;
        }

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
