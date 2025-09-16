namespace InventorySystem.Application.Features.Inventory.Dtos
{
    public class StockAdjustmentLogDto
    {

        public int Id { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;


        public int QuantityChange { get; set; }

        public string Reason { get; set; } = null!;

        public DateTime AdjustedAt { get; set; }

        public int? AdjustedById { get; set; }
        public string AdjustedByName { get; set; } = null!;
    }
}
