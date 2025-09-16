using Microsoft.EntityFrameworkCore;
using InventorySystem.Application.Common.Interfaces;

namespace Infrastructure.Services.Common.Helpers
{
    internal class ConcurrencyHelper : IConcurrencyHelper
    {
        public async Task ExecuteWithRetryAsync(Func<Task> action, int maxTries = 3)
        {


            int count = 0;


            while (true)
            {


                try
                {
                    await action();
                    break;
                }

                catch (DbUpdateConcurrencyException)
                {

                    count++;

                    if (count == maxTries)
                        throw;

                    await Task.Delay(100 * count);
                }
            }

        }
    }
}
