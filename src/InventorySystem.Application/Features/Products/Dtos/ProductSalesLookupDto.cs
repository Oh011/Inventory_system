namespace InventorySystem.Application.Features.Products.Dtos
{
    public class ProductSalesLookupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Barcode { get; set; }
        public string Unit { get; set; } = default!;

        public decimal SellingPrice { get; set; }           // Price to charge customer
        public string? ProductImageUrl { get; set; }        // Optional for UI display
        public int QuantityInStock { get; set; }            // Important to prevent overselling

        public int? CategoryId { get; set; }
    }

}
