using Application.Exceptions;
using Domain.Entities;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using InventorySystem.Application.Features.Products.Dtos;
using MediatR;

namespace InventorySystem.Application.Features.SalesInvoice.Queries.GetProductByBarcodeForSales
{
    internal class GetProductByBarcodeForSalesQueryHandler : IRequestHandler<GetProductByBarcodeForSalesQuery, ProductSalesLookupDto>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IUriService uriService;



        public GetProductByBarcodeForSalesQueryHandler(IUnitOfWork unitOfWork, IUriService uriService)
        {
            this.unitOfWork = unitOfWork;
            this.uriService = uriService;

        }
        public async Task<ProductSalesLookupDto> Handle(GetProductByBarcodeForSalesQuery request, CancellationToken cancellationToken)
        {


            var repository = unitOfWork.GetRepository<Product, int>();

            var product = await repository.FirstOrDefaultAsync(p => p.Barcode == request.Barcode
           , p => new ProductSalesLookupDto
           {
               Id = p.Id,
               Name = p.Name,
               Barcode = p.Barcode,
               Unit = p.Unit.ToString(),
               SellingPrice = p.SellingPrice,
               ProductImageUrl = p.ProductImageUrl,
               QuantityInStock = p.QuantityInStock,
               CategoryId = p.CategoryId
           });


            if (product == null)
                throw new NotFoundException("Product not found");



            product.ProductImageUrl = uriService.GetAbsoluteUri(product.ProductImageUrl);

            return product;

        }
    }
}
