using FluentValidation;

namespace InventorySystem.Application.Features.PurchaseOrders.Commands.Create
{
    public class CreatePurchaseOrderCommandValidator : AbstractValidator<CreatePurchaseOrderCommand>
    {
        public CreatePurchaseOrderCommandValidator()
        {
            RuleFor(x => x.SupplierId)
                .GreaterThan(0).WithMessage("Supplier is required.");

            RuleFor(x => x.ExpectedDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .When(x => x.ExpectedDate.HasValue)
                .WithMessage("Expected date cannot be in the past.");

            RuleFor(x => x.DeliveryFee)
                .GreaterThanOrEqualTo(0).WithMessage("Delivery fee cannot be negative.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("At least one purchase item is required.");

            RuleForEach(x => x.Items).SetValidator(new PurchaseOrderItemDtoValidator());
        }
    }
}
