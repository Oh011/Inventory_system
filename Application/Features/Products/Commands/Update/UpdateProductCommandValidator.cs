using FluentValidation;

namespace Project.Application.Features.Products.Commands.Update
{
    internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductRequest>
    {




        public UpdateProductCommandValidator()
        {




            RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Product ID must be greater than zero.");

            RuleFor(x => x.Name)
                .MaximumLength(100)
                .When(x => x.Name != null)
                .WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => x.Description != null);

            RuleFor(x => x.Barcode)
                .MaximumLength(50)
                .When(x => x.Barcode != null);

            RuleFor(x => x.Unit)
                .IsInEnum()
                .When(x => x.Unit.HasValue);

            RuleFor(x => x.CostPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.CostPrice.HasValue);

            RuleFor(x => x.SellingPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.SellingPrice.HasValue);

            RuleFor(x => x.MinimumStock)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinimumStock.HasValue);

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .When(x => x.CategoryId.HasValue);



        }
    }
}
