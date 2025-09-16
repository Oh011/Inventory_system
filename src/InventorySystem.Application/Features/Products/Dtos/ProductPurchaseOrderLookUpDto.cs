namespace InventorySystem.Application.Features.Products.Dtos
{
    public class ProductPurchaseOrderLookUpDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Barcode { get; set; }
        public string Unit { get; set; } = default!;
        public decimal CostPrice { get; set; }
        public int QuantityInStock { get; set; }

        public int? CategoryId { get; set; }
    }
}
