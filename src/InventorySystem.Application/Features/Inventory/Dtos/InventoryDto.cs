namespace InventorySystem.Application.Features.Inventory.Dtos
{
    public class InventoryDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string? Barcode { get; set; }
        public string Unit { get; set; } = default!;
        public string CategoryName { get; set; } = default!;

        // Stock
        public int CurrentStock { get; set; }
        public int ReorderThreshold { get; set; }
        public bool IsCritical => CurrentStock <= ReorderThreshold;

        // Pricing
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal StockValue => CurrentStock * CostPrice;
    }

}
