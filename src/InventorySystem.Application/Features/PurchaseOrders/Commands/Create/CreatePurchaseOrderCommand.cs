using MediatR;
using Project.Application.Features.PurchaseOrders.Dtos;

namespace Project.Application.Features.PurchaseOrders.Commands.Create
{
    public class CreatePurchaseOrderCommand : IRequest<PurchaseOrderResultDto>
    {

        public int SupplierId { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public decimal DeliveryFee { get; set; }

        public string? Notes { get; set; }


        public int? CreatedByEmployeeId { get; set; }
        public List<PurchaseOrderItemDto> Items { get; set; } = new();

    }
}
