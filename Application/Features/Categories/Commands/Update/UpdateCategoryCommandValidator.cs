using FluentValidation;

namespace Project.Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Category ID must be greater than zero.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Category name is required.")
                .MaximumLength(100)
                .WithMessage("Category name must not exceed 100 characters.");

            // Description is optional, but if provided, limit its length
            RuleFor(x => x.Description)
                .MaximumLength(250)
                .WithMessage("Description must not exceed 250 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
