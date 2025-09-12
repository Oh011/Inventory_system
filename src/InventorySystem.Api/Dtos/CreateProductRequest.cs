using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Dtos
{
    public class CreateProductRequest
    {

        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }
        public string? Barcode { get; set; }
        public UnitType Unit { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int MinimumStock { get; set; }
        public int? CategoryId { get; set; }

        // ASP.NET Core only
        public IFormFile? Image { get; set; }
        public int QuantityInStock { get; set; } = 0;
    }
}
