using FluentValidation;
using Shared.Dtos;

namespace InventorySystem.Application.Features.Employees.Commands.UpdateEmployeeImage
{
    internal class UpdateEmployeeProfileImageCommandValidator : AbstractValidator<UpdateEmployeeProfileImageCommand>
    {

        private readonly List<string> _AllowedExtensions = new List<string>()
        {


            ".png",".jpg",".jpeg"
        };



        private int _MaxSize = 2_097_152; //--> 2MB {written in bytes}



        public UpdateEmployeeProfileImageCommandValidator()
        {

            RuleFor(x => x.EmployeeId)
         .GreaterThan(0)
         .WithMessage("Employee ID must be greater than 0.");


            RuleFor(x => x.ProfileImage)
          .Must(BeAValidImage).When(x => x.ProfileImage is not null)
          .WithMessage($"Only images with extensions: {string.Join(", ", _AllowedExtensions)} are allowed.");

            RuleFor(x => x.ProfileImage)
                .Must(f => f == null || f.FileLength <= _MaxSize)
                .WithMessage("Image size must not exceed 2MB.");
        }


        private bool BeAValidImage(FileUploadDto? file)
        {
            if (file is null) return true;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return _AllowedExtensions.Contains(extension);
        }
    }
}
