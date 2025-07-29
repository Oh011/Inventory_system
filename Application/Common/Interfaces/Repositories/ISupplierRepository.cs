
using Domain.Entities;

namespace Project.Application.Common.Interfaces.Repositories
{
    public interface ISupplierRepository
    {


        Task<Supplier?> GetSupplierWithDetailsAsync(int id);
    }
}
