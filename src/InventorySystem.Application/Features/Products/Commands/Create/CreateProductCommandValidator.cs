using FluentValidation;
using Shared.Dtos;

namespace InventorySystem.Application.Features.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {

        private readonly List<string> _AllowedExtensions = new List<string>()
        {


            ".png",".jpg",".jpeg"
        };



        private int _MaxSize = 2_097_152; //--> 2MB {written in bytes}


        public CreateProductCommandValidator()
        {



            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(x => x.Barcode)
                .MaximumLength(50).WithMessage("Barcode must not exceed 50 characters.");

            RuleFor(x => x.CostPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Cost price must be non-negative.");

            RuleFor(x => x.SellingPrice)
                .GreaterThan(0).WithMessage("Selling price must be greater than 0.");

            RuleFor(x => x.QuantityInStock)
                .GreaterThanOrEqualTo(0).WithMessage("QuantitySold in stock must be non-negative.");

            RuleFor(x => x.MinimumStock)
                .GreaterThanOrEqualTo(0).WithMessage("Minimum stock must be non-negative.");



            RuleFor(x => x.SellingPrice)
    .GreaterThan(x => x.CostPrice)
    .WithMessage("Selling price must be greater than cost price.");


            RuleFor(x => x.Unit)
                .IsInEnum().WithMessage("Invalid unit type.");


            RuleFor(x => x.Image)
          .Must(BeAValidImage).When(x => x.Image is not null)
          .WithMessage($"Only images with extensions: {string.Join(", ", _AllowedExtensions)} are allowed.");

            RuleFor(x => x.Image)
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
