using FluentValidation;

namespace InventorySystem.Application.Features.SalesReturn.Commands.Create
{
    internal class CreateSalesReturnCommandValidator : AbstractValidator<CreateSalesReturnCommand>
    {


        public CreateSalesReturnCommandValidator()
        {

            RuleFor(x => x.SalesInvoiceId)
                .GreaterThan(0)
                .WithMessage("Sales invoice id must be greater than 0.");


            RuleFor(x => x.Reason)
                .MaximumLength(250)
                .WithMessage("Reason cannot exceed 250 characters.");

            // Items must not be empty
            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("At least one item must be returned.");

            // Apply validation for each item
            RuleForEach(x => x.Items)
                .SetValidator(new CreateSalesReturnItemDtoValidator());
        }
    }
}
