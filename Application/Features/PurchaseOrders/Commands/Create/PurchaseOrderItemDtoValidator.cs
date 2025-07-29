using FluentValidation;
using Project.Application.Features.PurchaseOrders.Dtos;

namespace Project.Application.Features.PurchaseOrders.Commands.Create
{
    public class PurchaseOrderItemDtoValidator : AbstractValidator<PurchaseOrderItemDto>
    {
        public PurchaseOrderItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Product ID is required.");

            RuleFor(x => x.QuantityOrdered)
                .GreaterThan(0).WithMessage("Ordered quantity must be greater than zero.");

            RuleFor(x => x.UnitCost)
                .GreaterThanOrEqualTo(0).WithMessage("Unit cost cannot be negative.");
        }
    }

}
