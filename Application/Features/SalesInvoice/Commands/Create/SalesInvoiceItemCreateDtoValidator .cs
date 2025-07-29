using FluentValidation;
using Project.Application.Features.SalesInvoice.Dtos;

namespace Project.Application.Features.SalesInvoice.Commands.Create
{


    public class SalesInvoiceItemCreateDtoValidator : AbstractValidator<SalesInvoiceItemCreateDto>
    {
        public SalesInvoiceItemCreateDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Product ID is required.");

            RuleFor(x => x.QuantitySold)
                .GreaterThan(0).WithMessage("Quantity sold must be greater than zero.");

            RuleFor(x => x.SellingPrice)
                .GreaterThan(0).WithMessage("Selling price must be greater than zero.");

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0).When(x => x.Discount.HasValue)
                .WithMessage("Discount must be zero or a positive value.");
        }
    }

}
