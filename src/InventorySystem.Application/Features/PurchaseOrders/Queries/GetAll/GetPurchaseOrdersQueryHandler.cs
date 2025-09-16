using MediatR;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using InventorySystem.Application.Features.PurchaseOrders.Interfaces;
using Shared.Results;

namespace InventorySystem.Application.Features.PurchaseOrders.Queries.GetAll
{
    internal class GetPurchaseOrdersQueryHandler : IRequestHandler<GetPurchaseOrdersQuery, PaginatedResult<PurchaseOrderListDto>>
    {


        private readonly IPurchaseOrderService _purchaseOrderService;


        public GetPurchaseOrdersQueryHandler(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }
        public Task<PaginatedResult<PurchaseOrderListDto>> Handle(GetPurchaseOrdersQuery request, CancellationToken cancellationToken)
        {


            var orders = _purchaseOrderService.GetAllPurchaseOrders(request);



            return orders;
        }
    }
}
