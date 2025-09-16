using Application.Exceptions;
using Domain.Enums;
using MediatR;
using InventorySystem.Application.Common.Interfaces.PdfGenerators;
using InventorySystem.Application.Features.PurchaseOrders.Interfaces;

namespace InventorySystem.Application.Features.PurchaseOrders.Queries.ExportPf
{
    public class ExportPurchaseOrderPdfQueryHandler : IRequestHandler<ExportPurchaseOrderPdfQuery, byte[]>
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IPurchaseOrderPdfGenerator _pdfGenerator;

        public ExportPurchaseOrderPdfQueryHandler(
            IPurchaseOrderService purchaseOrderService,
            IPurchaseOrderPdfGenerator pdfGenerator)
        {
            _purchaseOrderService = purchaseOrderService;
            _pdfGenerator = pdfGenerator;
        }

        public async Task<byte[]> Handle(ExportPurchaseOrderPdfQuery request, CancellationToken cancellationToken)
        {
            var order = await _purchaseOrderService.GetPurchaseOrderById(request.PurchaseOrderId);

            if (order == null)
                throw new NotFoundException("Purchase order not found");



            if (order.Status == PurchaseOrderStatus.Received.ToString())
                return _pdfGenerator.GenerateReceivedOrderPdf(order);



            return _pdfGenerator.GenerateCreatedOrderPdf(order);
        }
    }

}
