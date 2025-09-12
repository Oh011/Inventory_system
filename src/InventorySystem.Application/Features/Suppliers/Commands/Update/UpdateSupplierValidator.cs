using FluentValidation;

namespace Project.Application.Features.Suppliers.Commands.Update
{
    internal class UpdateSupplierValidator : AbstractValidator<UpdateSupplierRequest>
    {


        public UpdateSupplierValidator()
        {


            RuleFor(x => x.Id)
               .GreaterThan(0)
               .WithMessage("Category ID must be greater than zero.");

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
