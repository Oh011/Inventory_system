using MediatR;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;

namespace InventorySystem.Application.Features.PurchaseOrders.Queries.GetById
{
    public class GetPurchaseOrderByIdQuery : IRequest<PurchaseOrderDetailDto>
    {


        public int Id { get; set; }


        public GetPurchaseOrderByIdQuery(int id)
        {
            Id = id;
        }
    }
}
