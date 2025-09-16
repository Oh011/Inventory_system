using FluentValidation;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;

namespace InventorySystem.Application.Features.PurchaseOrders.Commands.Update
{
    public class UpdatePurchaseOrderItemDtoValidator : AbstractValidator<UpdatePurchaseOrderItemDto>
    {
        public UpdatePurchaseOrderItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Product ID must be greater than 0.");

            RuleFor(x => x.QuantityReceived)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity received must be zero or more.");
        }
    }
}
