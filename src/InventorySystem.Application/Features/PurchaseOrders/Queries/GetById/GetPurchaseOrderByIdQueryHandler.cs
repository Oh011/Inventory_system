using MediatR;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using InventorySystem.Application.Features.PurchaseOrders.Interfaces;

namespace InventorySystem.Application.Features.PurchaseOrders.Queries.GetById
{
    internal class GetPurchaseOrderByIdQueryHandler : IRequestHandler<GetPurchaseOrderByIdQuery, PurchaseOrderDetailDto>
    {


        private readonly IPurchaseOrderService purchaseOrderService;


        public GetPurchaseOrderByIdQueryHandler(IPurchaseOrderService purchaseOrderService)
        {
            this.purchaseOrderService = purchaseOrderService;
        }
        public async Task<PurchaseOrderDetailDto> Handle(GetPurchaseOrderByIdQuery request, CancellationToken cancellationToken)
        {

            var result = await purchaseOrderService.GetPurchaseOrderById(request.Id);
            return result;


        }
    }
}
