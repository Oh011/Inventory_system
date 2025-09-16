using InventorySystem.Application.Features.PurchaseOrders.Dtos;

namespace InventorySystem.Application.Common.Interfaces.PdfGenerators
{
    public interface IPurchaseOrderPdfGenerator
    {
        byte[] GenerateCreatedOrderPdf(PurchaseOrderDetailDto order);
        byte[] GenerateReceivedOrderPdf(PurchaseOrderDetailDto order);
    }

}
