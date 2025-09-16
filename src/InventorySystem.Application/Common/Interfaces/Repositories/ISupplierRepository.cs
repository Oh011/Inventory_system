
using Domain.Entities;

namespace InventorySystem.Application.Common.Interfaces.Repositories
{
    public interface ISupplierRepository
    {


        Task<Supplier?> GetSupplierWithDetailsAsync(int id);
    }
}
