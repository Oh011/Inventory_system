namespace InventorySystem.Application.Features.PurchaseOrders.Dtos
{
    // For overall summary
    public class PurchaseOrderOverviewDto
    {
        public int PendingCount { get; set; }
        public int ReceivedCount { get; set; }
        public int PartiallyReceivedCount { get; set; }
        public int CancelledCount { get; set; }
        public decimal TotalPurchaseOrderValue { get; set; }
    }

}
