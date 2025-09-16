
using Domain.Enums;

namespace InventorySystem.Application.Features.Products.Dtos
{
    public class ProductBaseCommand
    {

        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? Barcode { get; set; }
        public UnitType Unit { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public int MinimumStock { get; set; }


        public int? CategoryId { get; set; }
    }
}
