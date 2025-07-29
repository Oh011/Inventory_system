using MediatR;
using Project.Application.Features.PurchaseOrders.Dtos;

namespace Project.Application.Features.PurchaseOrders.Commands.Update
{
    public class UpdatePurchaseOrderCommand : UpdatePurchaseOrderRequest, IRequest<PurchaseOrderResultDto>
    {


        public int Id { get; set; }



        public UpdatePurchaseOrderCommand(int id, string rowVersion, List<UpdatePurchaseOrderItemDto> Items) : base(rowVersion, Items)
        {

            this.Id = id;
            this.RowVersion = rowVersion;
            this.Items = Items;
        }
    }
}
