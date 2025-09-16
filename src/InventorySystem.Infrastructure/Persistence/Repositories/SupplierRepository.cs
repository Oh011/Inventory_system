using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using InventorySystem.Application.Common.Interfaces.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    internal class SupplierRepository : GenericRepository<Supplier, int>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Supplier?> GetSupplierWithDetailsAsync(int id)
        {


            return await _context.Suppliers.Where(s => s.Id == id)
                .Include(s => s.SupplierCategories!).ThenInclude(s => s.Category).FirstOrDefaultAsync();
        }



    }
}
