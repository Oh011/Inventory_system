
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Project.Application.Features.Inventory.Dtos;
using Project.Application.Features.Inventory.Queries.GetLowStock;
using Project.Application.Features.Products.Commands.Update;
using Project.Application.Features.Products.Dtos;
using Project.Application.Features.Products.queries.GetProducts;
using Shared.Results;

namespace Project.Application.Features.Products.Intrefaces
{
    public interface IProductService
    {



        Task UpdateProductsStock(IEnumerable<int> productIds);


        Task<PaginatedResult<LowStockProductDto>> GetLowStockProductsAsync(GetLowStockQuery request);

        Task<Product> CreateProduct(Product product, IFormFile? ProductImage);

        Task<PaginatedResult<ProductResultDto>> GetAllProducts(GetProductsQuery query);
        Task<Product> UpdateProduct(UpdateProductRequest request);
    }
}
