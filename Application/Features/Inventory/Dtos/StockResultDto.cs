namespace Project.Application.Features.Inventory.Dtos
{
    public class StockResult
    {
        public int NewQuantity { get; set; }

        public int Threshold { get; set; }
        public int OldQuantity { get; set; }
    }
}
