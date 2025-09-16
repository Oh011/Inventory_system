using InventorySystem.Application.Features.Inventory.Dtos;

namespace InventorySystem.Application.Features.Products.Intrefaces
{
    public interface IProductRepository
    {


        Task<StockResult?> AdjustProductStock(int id, int amount);
    }
}
