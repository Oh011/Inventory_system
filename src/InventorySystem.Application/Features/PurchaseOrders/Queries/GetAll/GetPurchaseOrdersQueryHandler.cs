using MediatR;
using Project.Application.Features.PurchaseOrders.Dtos;
using Project.Application.Features.PurchaseOrders.Interfaces;
using Shared.Results;

namespace Project.Application.Features.PurchaseOrders.Queries.GetAll
{
    internal class GetPurchaseOrdersQueryHandler : IRequestHandler<GetPurchaseOrdersQuery, PaginatedResult<PurchaseOrderSummaryDto>>
    {


        private readonly IPurchaseOrderService _purchaseOrderService;


        public GetPurchaseOrdersQueryHandler(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }
        public Task<PaginatedResult<PurchaseOrderSummaryDto>> Handle(GetPurchaseOrdersQuery request, CancellationToken cancellationToken)
        {


            var orders = _purchaseOrderService.GetAllPurchaseOrders(request);



            return orders;
        }
    }
}
