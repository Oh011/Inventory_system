using Domain.Enums;

namespace InventorySystem.Application.Features.SalesInvoice.Dtos
{
    public class SalesInvoiceDetailsDto
    {
        public int Id { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string? CustomerName { get; set; }

        public int? CustomerId { get; set; }

        public string? CreatedByEmployeeName { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal? InvoiceDiscount { get; set; }

        public decimal? DeliveryFee { get; set; }

        public decimal FinalTotal => TotalAmount - (InvoiceDiscount ?? 0) + (DeliveryFee ?? 0);

        public PaymentMethod PaymentMethod { get; set; }

        public string? Notes { get; set; }

        public List<SalesInvoiceItemDto> Items { get; set; } = [];
    }

}
