namespace Project.Application.Features.Products.Dtos
{
    public class ProductResultDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;


        public string? Barcode { get; set; }

        public string Unit { get; set; } = default!; // string version of UnitType enum



        public decimal SellingPrice { get; set; }



        public string? ProductImageUrl { get; set; }

        public int? CategoryId { get; set; }

        public string? CategoryName { get; set; } // Optional: to show category name


        public bool IsCategoryDeleted { get; set; }


    }

}
