using FluentValidation;
using InventorySystem.Application.Features.SalesReturn.Dtos;

namespace InventorySystem.Application.Features.SalesReturn.Commands.Create
{
    internal class CreateSalesReturnItemDtoValidator
      : AbstractValidator<CreateSalesReturnItemDto>
    {
        public CreateSalesReturnItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Product id must be greater than 0.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("QuantitySold must be greater than 0.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Unit price must be non-negative.");
        }
    }
}
