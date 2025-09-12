using MediatR;
using Project.Application.Features.PurchaseOrders.Dtos;

namespace Project.Application.Features.PurchaseOrders.Queries.GetById
{
    public class GetPurchaseOrderByIdQuery : IRequest<PurchaseOrderResultDto>
    {


        public int Id { get; set; }


        public GetPurchaseOrderByIdQuery(int id)
        {
            Id = id;
        }
    }
}
