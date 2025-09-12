namespace Project.Application.Features.SalesInvoice.Dtos
{
    public class SalesInvoiceItemDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = default!;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal Subtotal => (UnitPrice * Quantity) - Discount;
    }

}
