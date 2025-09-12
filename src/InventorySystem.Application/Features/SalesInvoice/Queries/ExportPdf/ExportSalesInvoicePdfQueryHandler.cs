using MediatR;
using Project.Application.Common.Interfaces.PdfGenerators;
using Project.Application.Features.SalesInvoice.Interfaces;

namespace Project.Application.Features.SalesInvoice.Queries.ExportPdf
{
    internal class ExportSalesInvoicePdfQueryHandler : IRequestHandler<ExportSalesInvoicePdfQuery, byte[]>
    {


        private readonly ISalesInvoicePdfGenerator _pdfGenerator;
        private readonly ISalesInvoiceService salesInvoiceService;


        public ExportSalesInvoicePdfQueryHandler(ISalesInvoicePdfGenerator salesInvoicePdfGenerator, ISalesInvoiceService salesInvoiceService)
        {
            this._pdfGenerator = salesInvoicePdfGenerator;
            this.salesInvoiceService = salesInvoiceService;

        }
        public async Task<byte[]> Handle(ExportSalesInvoicePdfQuery request, CancellationToken cancellationToken)
        {

            var invoice = await salesInvoiceService.GetInvoiceById(request.InvoiceId);


            var pdf = _pdfGenerator.GenerateSalesInvoicePdf(invoice);


            return pdf;

        }
    }
}
