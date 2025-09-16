
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using InventorySystem.Application.Common.Interfaces;
using System.Text.Json;

namespace Infrastructure.Persistence.DataSeeding
{
    internal class DbInitializer(
          ApplicationDbContext _dbContext) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            try
            {



                if (_dbContext.Database.GetPendingMigrations().Any())
                    await _dbContext.Database.MigrateAsync();




                if (!_dbContext.Category.Any())
                {

                    var categoriesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\DataSeeding\Categories.json");

                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);


                    if (categories is not null && categories.Any())
                    {

                        await _dbContext.Category.AddRangeAsync(categories);
                        await _dbContext.SaveChangesAsync();
                    }
                }




                if (!_dbContext.Suppliers.Any())
                {

                    var suppliersData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\DataSeeding\Categories.json");

                    var suppliers = JsonSerializer.Deserialize<List<SupplierSeedDto>>(suppliersData);


                    if (suppliers is not null && suppliers.Any())
                    {


                        foreach (var s in suppliers)
                        {


                            var supplier = new Supplier
                            {

                                Name = s.Name,
                                ContactName = s.ContactName,
                                Phone = s.Phone,
                                Email = s.Email,
                                Address = s.Address,
                                CreatedAt = DateTime.UtcNow,
                                SupplierCategories = s.CategoryIds.Select(catId => new SupplierCategory
                                {
                                    SupplierId = s.Id,
                                    CategoryId = catId
                                }).ToList()
                            };

                            _dbContext.Suppliers.Add(supplier);
                        }


                        await _dbContext.SaveChangesAsync();

                    }
                }







                if (!_dbContext.Products.Any())
                {

                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\DataSeeding\Products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);


                    if (products is not null && products.Any())
                    {

                        await _dbContext.Products.AddRangeAsync(products);
                        await _dbContext.SaveChangesAsync();
                    }
                }


            }

            catch (Exception ex)
            {
                throw;

            }

        }
    }
}

