namespace Project.Application.Features.Inventory.Dtos
{
    public class InventoryValueDto
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string Unit { get; set; } = default!;
        public decimal CostPrice { get; set; }
        public int QuantityInStock { get; set; }
        public decimal TotalValue => CostPrice * QuantityInStock;

        public string? CategoryName { get; set; }
    }
}
