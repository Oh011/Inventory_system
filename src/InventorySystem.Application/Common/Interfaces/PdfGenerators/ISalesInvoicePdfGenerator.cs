using InventorySystem.Application.Features.SalesInvoice.Dtos;

namespace InventorySystem.Application.Common.Interfaces.PdfGenerators
{
    public interface ISalesInvoicePdfGenerator
    {

        byte[] GenerateSalesInvoicePdf(SalesInvoiceDetailsDto order);
    }
}
