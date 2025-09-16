using FluentValidation;

namespace InventorySystem.Application.Features.SalesInvoice.Commands.Create
{


    public class CreateSalesInvoiceCommandValidator : AbstractValidator<CreateSalesInvoiceCommand>
    {
        public CreateSalesInvoiceCommandValidator()
        {
            RuleFor(x => x.InvoiceDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Invoice date cannot be in the future.");

            RuleFor(x => x.Items)
                .NotNull().WithMessage("Invoice must contain at least one item.")
                .NotEmpty().WithMessage("Invoice must contain at least one item.");

            RuleForEach(x => x.Items).SetValidator(new SalesInvoiceItemCreateDtoValidator());

            RuleFor(x => x.DiscountAmount)
                .GreaterThanOrEqualTo(0).When(x => x.DiscountAmount.HasValue)
                .WithMessage("Discount amount must be a positive number.");
        }
    }

}
