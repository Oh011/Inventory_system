namespace InventorySystem.Application.Features.Inventory.Dtos
{
    public class InventoryDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public int CurrentStock { get; set; }
        public string Unit { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
    }

}
