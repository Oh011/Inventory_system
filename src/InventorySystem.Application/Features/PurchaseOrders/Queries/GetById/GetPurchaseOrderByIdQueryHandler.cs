using MediatR;
using Project.Application.Features.PurchaseOrders.Dtos;
using Project.Application.Features.PurchaseOrders.Interfaces;

namespace Project.Application.Features.PurchaseOrders.Queries.GetById
{
    internal class GetPurchaseOrderByIdQueryHandler : IRequestHandler<GetPurchaseOrderByIdQuery, PurchaseOrderResultDto>
    {


        private readonly IPurchaseOrderService purchaseOrderService;


        public GetPurchaseOrderByIdQueryHandler(IPurchaseOrderService purchaseOrderService)
        {
            this.purchaseOrderService = purchaseOrderService;
        }
        public async Task<PurchaseOrderResultDto> Handle(GetPurchaseOrderByIdQuery request, CancellationToken cancellationToken)
        {

            var result = await purchaseOrderService.GetPurchaseOrderById(request.Id);
            return result;


        }
    }
}
