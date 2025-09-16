using FluentValidation;

namespace InventorySystem.Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {



        public ChangePasswordCommandValidator()
        {



            RuleFor(x => x.UserId).NotEmpty().
                WithMessage("User Id is required."); ;


            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Password is required.");





            RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");



        }


    }
}
