using FluentValidation;

namespace InventorySystem.Application.Features.PurchaseOrders.Commands.Update
{
    public class UpdatePurchaseOrderCommandValidator : AbstractValidator<UpdatePurchaseOrderCommand>
    {
        public UpdatePurchaseOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Purchase order ID must be greater than 0.");

            RuleFor(x => x.RowVersion)
                .NotEmpty()
                .WithMessage("Row version is required.");

            RuleFor(x => x.Items)
                .NotNull()
                .WithMessage("Items list is required.")
                .Must(items => items.Count > 0)
                .WithMessage("At least one item must be provided.");

            RuleForEach(x => x.Items).SetValidator(new UpdatePurchaseOrderItemDtoValidator());
        }
    }
}
