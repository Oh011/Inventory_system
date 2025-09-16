using FluentValidation;

namespace InventorySystem.Application.Features.Suppliers.Commands.Create
{

    public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Supplier name is required.")
                .MaximumLength(100).WithMessage("Name can't exceed 100 characters.");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Invalid email format.");

            RuleFor(x => x.Phone)
                .Matches(@"^\+?[0-9]{7,15}$").When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Invalid phone number.");


        }
    }

}



