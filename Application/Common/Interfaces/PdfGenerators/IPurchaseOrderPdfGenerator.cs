using Project.Application.Features.PurchaseOrders.Dtos;

namespace Project.Application.Common.Interfaces.PdfGenerators
{
    public interface IPurchaseOrderPdfGenerator
    {
        byte[] GenerateCreatedOrderPdf(PurchaseOrderResultDto order);
        byte[] GenerateReceivedOrderPdf(PurchaseOrderResultDto order);
    }

}
