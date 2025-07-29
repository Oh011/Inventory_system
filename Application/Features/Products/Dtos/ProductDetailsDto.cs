using Project.Application.Features.PurchaseOrders.Dtos;
using Project.Application.Features.SalesInvoice.Dtos;

namespace Project.Application.Features.Products.Dtos
{
    public class ProductDetailsDto : ProductResultDto
    {
        public DateTime CreatedAt { get; set; }

        // Optional: Include related entities (lightweight versions)
        public List<PurchaseOrderSummaryDto>? PurchaseOrders { get; set; }
        public List<SalesInvoiceSummaryDto>? SalesInvoices { get; set; }

        // Optional: Audit info
        // public string? CreatedBy { get; set; }
        // public DateTime? LastModifiedAt { get; set; }
    }

}
