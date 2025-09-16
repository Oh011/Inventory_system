using InventorySystem.Application.Features.PurchaseOrders.Dtos;
using InventorySystem.Application.Features.SalesInvoice.Dtos;

namespace InventorySystem.Application.Features.Products.Dtos
{
    public class ProductDetailsDto : ProductResultDto
    {
        public DateTime CreatedAt { get; set; }

        // Optional: Include related entities (lightweight versions)
        public List<PurchaseOrderListDto>? PurchaseOrders { get; set; }
        public List<SalesInvoiceSummaryDto>? SalesInvoices { get; set; }

        // Optional: Audit info
        // public string? CreatedBy { get; set; }
        // public DateTime? LastModifiedAt { get; set; }
    }

}
