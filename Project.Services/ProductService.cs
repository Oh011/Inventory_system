using Application.Exceptions;
using Application.Specifications.Products;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Common.Validators;
using Project.Application.Features.Inventory.Dtos;
using Project.Application.Features.Inventory.Queries.GetLowStock;
using Project.Application.Features.Inventory.Specifications;
using Project.Application.Features.Products.Commands.Update;
using Project.Application.Features.Products.Dtos;
using Project.Application.Features.Products.Intrefaces;
using Project.Application.Features.Products.queries.GetProducts;
using Shared.Results;

namespace Project.Services
{
    class ProductService(IUnitOfWork unitOfWork, IEntityValidator<Category> categoryValidator, IMapper mapper, IUploadService uploadService) : IProductService
    {





        public async Task<Product> CreateProduct(Product product, IFormFile? ProductImage)
        {



            var productRepository = unitOfWork.GetRepository<Product, int>();


            if (product.CategoryId.HasValue)
            {

                await categoryValidator.ValidateExistsAsync(product.CategoryId.Value, "category");
            }


            if (await productRepository.ExistsAsync(p => p.Barcode == product.Barcode))
            {
                throw new ConflictException($"Product with barcode '{product.Barcode}' already exists.");
            }


            if (ProductImage != null)
            {

                product.ProductImageUrl = await UploadProductImage(ProductImage);
            }


            await productRepository.AddAsync(product);

            await unitOfWork.Commit();


            return product;



        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProducts(GetProductsQuery query)
        {


            var productRepository = unitOfWork.GetRepository<Product, int>();

            var specifications = new ProductsSpecifications(query);
            var result = await productRepository.GetAllWithProjectionSpecifications(specifications);
            var totalCount = await productRepository.CountAsync(specifications.Criteria);



            return new PaginatedResult<ProductResultDto>(query.PageIndex, query.pageSize,
                totalCount, result);



        }

        public async Task<PaginatedResult<LowStockProductDto>> GetLowStockProductsAsync(GetLowStockQuery request)
        {


            var repository = unitOfWork.GetRepository<Product, int>();

            var specifications = new LowStockInventorySpecification(request);


            var result = await repository.GetAllWithProjectionSpecifications(specifications);
            var totalCount = await repository.CountAsync(specifications.Criteria);



            return new PaginatedResult<LowStockProductDto>(

                request.PageIndex, request.pageSize, totalCount, result
                );
        }

        public async Task<Product> UpdateProduct(UpdateProductRequest request)
        {

            var productRepository = unitOfWork.GetRepository<Product, int>();


            var product = await productRepository.GetById(request.Id);



            if (product == null)
                throw new NotFoundException($"Product with Id :{request.Id} not found");


            if (request.CategoryId.HasValue)
            {

                await categoryValidator.ValidateExistsAsync(request.CategoryId.Value, "category");
            }


            mapper.Map(request, product);


            productRepository.Update(product);


            await unitOfWork.Commit();



            return product;



        }

        public Task UpdateProductsStock(IEnumerable<int> productIds)
        {
            throw new NotImplementedException();
        }

        private async Task<string?> UploadProductImage(IFormFile? ProductImage)
        {
            string? filePath = null;
            filePath = await uploadService.Upload(ProductImage, "Products");
            return filePath;

        }
    }
}
