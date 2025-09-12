using MediatR;

namespace Project.Application.Features.Inventory.Commands.AdjustInventory
{
    public class AdjustInventoryCommand : IRequest<string>
    {

        public int ProductId { get; set; }
        public int QuantityChange { get; set; } // Can be negative
        public string Reason { get; set; } = null!;
    }
}
