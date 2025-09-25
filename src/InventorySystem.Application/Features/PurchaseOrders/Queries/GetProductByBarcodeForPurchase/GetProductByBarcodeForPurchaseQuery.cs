using InventorySystem.Application.Features.Products.Dtos;
using MediatR;

namespace InventorySystem.Application.Features.PurchaseOrders.Queries.GetProductByBarcodeForPurchase
{
    public record GetProductByBarcodeForPurchaseQuery(int SupplierId, string Barcode)
       : IRequest<ProductPurchaseOrderLookUpDto>;

}
