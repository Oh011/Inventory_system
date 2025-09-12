using Domain.Common;

namespace Domain.Entities
{
    public class StockAdjustmentLog : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int QuantityChange { get; set; }
        public string Reason { get; set; } = null!;
        public DateTime AdjustedAt { get; set; } = DateTime.UtcNow;

        public int? AdjustedById { get; set; }
        public Employee AdjustedBy { get; set; } = null!;
    }

}
