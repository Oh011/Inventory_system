using FluentValidation;

namespace Project.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
    {


        public UpdateEmployeeValidator()
        {



            RuleFor(x => x.FullName)
                .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");



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
