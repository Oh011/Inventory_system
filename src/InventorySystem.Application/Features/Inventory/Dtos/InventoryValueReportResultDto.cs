namespace InventorySystem.Application.Features.Inventory.Dtos
{
    public class InventoryValueReportResultDto
    {
        public List<InventoryValueDto> Items { get; set; } = new();

        public decimal TotalInventoryValue => Items.Sum(i => i.TotalValue);
        public decimal AverageCostPrice => Items.Any() ? Items.Average(i => i.CostPrice) : 0;
    }

}
