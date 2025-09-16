using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Inventory.Dtos;

namespace InventorySystem.Application.Common.Interfaces.Services.Interfaces
{

    public interface IInventoryService
    {
        public Task<IEnumerable<int>> AdjustStockAsync(List<InventoryStockAdjustmentDto> adjustments, ITransactionManager transactionManager);
    }

}
