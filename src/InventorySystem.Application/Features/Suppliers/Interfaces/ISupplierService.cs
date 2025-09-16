using Domain.Entities;
using InventorySystem.Application.Features.Suppliers.Dtos;
using InventorySystem.Application.Features.Suppliers.Queries.GetSuppliers;
using Shared.Results;

namespace InventorySystem.Application.Features.Suppliers.Interfaces
{
    public interface ISupplierService
    {


        Task<Supplier> CreateSupplier(Supplier supplier);



        Task<SupplierBriefDto> GetSupplierBrief(int id);

        Task<PaginatedResult<Supplier>> GetAllSuppliers(GetSuppliersQuery query);

        Task<SupplierDetailDto> GetSupplierById(int id);

        Task<Supplier> UpdateSupplier(Supplier supplier);

    }
}
