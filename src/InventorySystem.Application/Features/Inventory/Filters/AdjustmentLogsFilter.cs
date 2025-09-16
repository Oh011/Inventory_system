using InventorySystem.Application.Common.Enums.SortOptions;

namespace InventorySystem.Application.Features.Inventory.Filters
{
    public class AdjustmentLogsFilter
    {
        public int? ProductId { get; set; }
        public int? AdjustedById { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? MinChange { get; set; }
        public int? MaxChange { get; set; }
        public AdjustmentLogsSortOptions? SortOptions { get; set; }
    }

}
