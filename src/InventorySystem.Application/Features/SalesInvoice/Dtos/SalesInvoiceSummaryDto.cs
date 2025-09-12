namespace Project.Application.Features.SalesInvoice.Dtos
{
    public class SalesInvoiceSummaryDto
    {
        public int Id { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string? CustomerName { get; set; }

        public string? CreatedByEmployeeName { get; set; }

        public decimal FinalTotal { get; set; }

        public string PaymentMethod { get; set; }

        public int? CustomerId { get; set; }
    }

}
