namespace InventorySystem.Application.Features.Inventory.Dtos
{
    public class InventoryStockAdjustmentDto
    {

        public int ProductId { get; set; }
        public int QuantityChange { get; set; } // Can be negative
    }
}
