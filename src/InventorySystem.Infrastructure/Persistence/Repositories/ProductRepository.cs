using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Project.Application.Features.Inventory.Dtos;
using Project.Application.Features.Products.Intrefaces;

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
        .SqlQuery<StockResult>($@"
    IF EXISTS (SELECT 1 FROM Products WHERE Id = {id})
    BEGIN
        IF (SELECT QuantityInStock FROM Products WHERE Id = {id}) + {amount} >= 0
        BEGIN
            UPDATE Products
            SET QuantityInStock = QuantityInStock + {amount}
            OUTPUT 
                inserted.QuantityInStock AS NewQuantity,
                deleted.QuantityInStock AS OldQuantity,
                inserted.MinimumStock    AS Threshold
            WHERE Id = {id};
        END
        ELSE
        BEGIN
            SELECT 
                QuantityInStock AS NewQuantity,
                QuantityInStock AS OldQuantity,
                MinimumStock    AS Threshold
            FROM Products
            WHERE Id = {id};
        END
    END
    ")
        .ToListAsync();

            return result.FirstOrDefault();


        }
    }
}
