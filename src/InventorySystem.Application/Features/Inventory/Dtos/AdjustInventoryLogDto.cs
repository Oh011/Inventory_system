namespace Project.Application.Features.Inventory.Dtos
{
    public class AdjustInventoryLogDto
    {

        public int ProductId { get; set; }      // Affected Product
        public int QuantityChange { get; set; } // Positive or negative change

        public string Reason { get; set; }

        public int AdjustedById { get; set; }

    }

}
