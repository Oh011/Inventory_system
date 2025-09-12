using FluentValidation;

namespace Project.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");

            RuleFor(x => x.Role)
      .NotEmpty().WithMessage("Role is required.");


            RuleFor(x => x.JobTitle)
                .MaximumLength(100).WithMessage("Job title must not exceed 100 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.JobTitle));

            RuleFor(x => x.Address)
                .MaximumLength(250).WithMessage("Address must not exceed 250 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Address));

            RuleFor(x => x.NationalId)
                .Matches(@"^\d{14}$").WithMessage("National ID must be 14 digits.")
                .When(x => !string.IsNullOrWhiteSpace(x.NationalId));
        }
    }
}
