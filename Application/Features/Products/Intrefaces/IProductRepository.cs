using Project.Application.Features.Inventory.Dtos;

namespace Project.Application.Features.Products.Intrefaces
{
    public interface IProductRepository
    {


        Task<StockResult?> AdjustProductStock(int id, int amount);
    }
}
