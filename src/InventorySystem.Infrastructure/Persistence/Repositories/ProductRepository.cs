using Infrastructure.Persistence.Context;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.Products.Intrefaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _context;


        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StockResult?> AdjustProductStock(int id, int amount)
        {



            var result = await _context.Database
    .SqlQueryRaw<StockResult>(
        @$"
        IF EXISTS (SELECT 1 FROM Products WHERE Id = @id)
        BEGIN
            IF (SELECT QuantityInStock FROM Products WHERE Id = @id) + @amount >= 0
            BEGIN
                UPDATE Products
                SET QuantityInStock = QuantityInStock + @amount
                OUTPUT 
                    inserted.QuantityInStock AS NewQuantity,
                    deleted.QuantityInStock AS OldQuantity,
                    inserted.MinimumStock    AS Threshold
                WHERE Id = @id;
            END
            ELSE
            BEGIN
                SELECT 
                    QuantityInStock AS NewQuantity,
                    QuantityInStock AS OldQuantity,
                    MinimumStock    AS Threshold
                FROM Products
                WHERE Id = @id;
            END
        END
        ",
        new SqlParameter("@id", id),
        new SqlParameter("@amount", amount))
    .ToListAsync();


            return result.FirstOrDefault();


        }
    }
}
