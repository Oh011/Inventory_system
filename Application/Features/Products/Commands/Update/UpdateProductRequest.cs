
using Domain.Enums;

namespace Project.Application.Features.Products.Commands.Update
{
    public class UpdateProductRequest
    {

        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Barcode { get; set; }
        public UnitType? Unit { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? SellingPrice { get; set; }
        public int? MinimumStock { get; set; }
        public int? CategoryId { get; set; }


    }
}
