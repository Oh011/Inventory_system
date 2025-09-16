using InventorySystem.Application.Features.Inventory.Dtos;
using InventorySystem.Application.Features.Notifications.Dtos;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using InventorySystem.Application.Features.SalesInvoice.Dtos;

namespace InventorySystem.Application.Features.Dashboard.Dtos
{
    public class DashboardDto
    {


        public int TotalProducts { get; set; }
        public decimal TotalStockValue { get; set; }

        public int TotalCustomers { get; set; }

        public int TotalEmployees { get; set; }
        public int LowStockCount { get; set; }
        public List<LowStockProductDto> LowStockProducts { get; set; }
        public decimal SalesTodayTotal { get; set; }
        public List<SalesInvoiceSummaryDto> RecentSalesInvoices { get; set; }
        public List<PurchaseOrderListDto> RecentPurchaseOrders { get; set; }
        public List<UserNotificationDto> RecentNotifications { get; set; }
    }
}
