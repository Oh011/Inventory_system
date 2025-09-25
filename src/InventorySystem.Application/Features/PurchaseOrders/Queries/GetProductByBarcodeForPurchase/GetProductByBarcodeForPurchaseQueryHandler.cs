using Application.Exceptions;
using Domain.Entities;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Products.Dtos;
using InventorySystem.Application.Features.Suppliers.Specifications;
using MediatR;

namespace InventorySystem.Application.Features.PurchaseOrders.Queries.GetProductByBarcodeForPurchase
{
    internal class GetProductByBarcodeForPurchaseQueryHandler : IRequestHandler<GetProductByBarcodeForPurchaseQuery, ProductPurchaseOrderLookUpDto>
    {


        private readonly IUnitOfWork unitOfWork;



        public GetProductByBarcodeForPurchaseQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ProductPurchaseOrderLookUpDto> Handle(GetProductByBarcodeForPurchaseQuery request, CancellationToken cancellationToken)
        {

            var repository = unitOfWork.GetRepository<Product, int>();

            var product = await repository.FirstOrDefaultAsync(p => p.Barcode == request.Barcode
           , p => new ProductPurchaseOrderLookUpDto
           {

               Id = p.Id,
               Name = p.Name,
               Barcode = p.Barcode,
               Unit = p.Unit.ToString(), // assuming Unit is an enum    
               QuantityInStock = p.QuantityInStock,
               CategoryId = p.CategoryId,
               CostPrice = p.CostPrice,

           });





            if (product == null)
                throw new NotFoundException("Product not found");



            var supplierCategoryRepository = unitOfWork.GetRepository<SupplierCategory, int>();

            var specifications = new SupplierCategoriesSpecifications(request.SupplierId);
            var categories = await supplierCategoryRepository.GetAllWithProjectionSpecifications(specifications);



            if (!categories.Any(c => c.Id == product.CategoryId))
                throw new BadRequestException($"Supplier {request.SupplierId} does not supply the product '{product.Name}' (barcode {product.Barcode}).");



            return product;

        }


    }
}
