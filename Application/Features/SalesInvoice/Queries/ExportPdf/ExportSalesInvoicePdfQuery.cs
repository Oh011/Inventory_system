using MediatR;

namespace Project.Application.Features.SalesInvoice.Queries.ExportPdf
{
    public class ExportSalesInvoicePdfQuery : IRequest<byte[]>
    {

        public int InvoiceId { get; set; }

        public ExportSalesInvoicePdfQuery(int id)
        {
            this.InvoiceId = id;
        }
    }
}
