using Project.Application.Features.Inventory.Dtos;

namespace Project.Application.Common.Interfaces.Services
{

    public interface IInventoryService
    {
        Task AdjustStockAsync(List<InventoryStockAdjustmentDto> adjustments);
    }

}
