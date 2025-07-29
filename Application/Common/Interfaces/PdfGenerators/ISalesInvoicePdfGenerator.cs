using Project.Application.Features.SalesInvoice.Dtos;

namespace Project.Application.Common.Interfaces.PdfGenerators
{
    public interface ISalesInvoicePdfGenerator
    {

        byte[] GenerateSalesInvoicePdf(SalesInvoiceDetailsDto order);
    }
}
