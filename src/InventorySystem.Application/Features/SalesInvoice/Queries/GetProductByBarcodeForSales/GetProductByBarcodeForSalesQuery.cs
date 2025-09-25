using InventorySystem.Application.Features.Products.Dtos;
using MediatR;

namespace InventorySystem.Application.Features.SalesInvoice.Queries.GetProductByBarcodeForSales
{
    public record GetProductByBarcodeForSalesQuery(string Barcode) : IRequest<ProductSalesLookupDto>;

}
