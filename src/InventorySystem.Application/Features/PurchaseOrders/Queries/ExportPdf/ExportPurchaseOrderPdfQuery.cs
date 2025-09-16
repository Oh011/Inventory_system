using MediatR;

namespace InventorySystem.Application.Features.PurchaseOrders.Queries.ExportPf
{
    public class ExportPurchaseOrderPdfQuery : IRequest<byte[]>
    {
        public int PurchaseOrderId { get; set; }

        public ExportPurchaseOrderPdfQuery(int purchaseOrderId)
        {
            PurchaseOrderId = purchaseOrderId;
        }
    }

}

//byte[] = raw binary data
