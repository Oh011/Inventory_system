
using Domain.Entities;
using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.Inventory.Queries.GetLowStock;
using InventorySystem.Application.Features.Products.Commands.Update;
using InventorySystem.Application.Features.Products.Dtos;
using InventorySystem.Application.Features.Products.queries.GetProducts;
using Shared.Dtos;
using Shared.Results;

namespace InventorySystem.Application.Features.Products.Intrefaces
{
    public interface IProductService
    {



        Task UpdateProductsStock(IEnumerable<int> productIds);


        Task<PaginatedResult<LowStockProductDto>> GetLowStockProductsAsync(GetLowStockQuery request);

        Task<Product> CreateProduct(Product product, FileUploadDto? ProductImage);

        Task<PaginatedResult<ProductResultDto>> GetAllProducts(GetProductsQuery query);
        Task<Product> UpdateProduct(UpdateProductCommand request);
    }
}
