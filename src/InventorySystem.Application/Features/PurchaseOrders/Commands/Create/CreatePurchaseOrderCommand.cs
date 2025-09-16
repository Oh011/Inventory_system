using MediatR;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;

namespace InventorySystem.Application.Features.PurchaseOrders.Commands.Create
{
    public class CreatePurchaseOrderCommand : IRequest<PurchaseOrderDetailDto>
    {

        public int SupplierId { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public decimal DeliveryFee { get; set; }

        public string? Notes { get; set; }


        public int? CreatedByEmployeeId { get; set; }
        public List<PurchaseOrderItemDto> Items { get; set; } = new();

    }
}
