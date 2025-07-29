namespace Project.Application.Features.Inventory.Dtos
{
    public class LowStockProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string? Unit { get; set; } = default!;
        public int QuantityInStock { get; set; }
        public int ReorderThreshold { get; set; }
        public string? CategoryName { get; set; }
    }
}


