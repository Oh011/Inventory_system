using MediatR;
using Project.Application.Common.Interfaces.PdfGenerators;
using Project.Application.Features.Reports.Sales.Interfaces;

namespace Project.Application.Features.Reports.Sales.Queries.ExportPdf
{
    internal class ExportSalesByProductReportPdfQueryHandler : IRequestHandler<ExportSalesByProductReportPdfQuery, byte[]>
    {

        private readonly ISalesReportService _reportService;
        private readonly ISalesReportPdfGenerator _pdfGenerator;

        public ExportSalesByProductReportPdfQueryHandler(ISalesReportService reportService, ISalesReportPdfGenerator pdfGenerator)
        {
            _reportService = reportService;
            _pdfGenerator = pdfGenerator;
        }

        public async Task<byte[]> Handle(ExportSalesByProductReportPdfQuery request, CancellationToken cancellationToken)
        {
            var reportData = await _reportService.GenerateReportAsync(request, cancellationToken);

            var pdf = _pdfGenerator.GenerateSalesByProductReportPdf(reportData, request.FromDate, request.ToDate); ;
            return pdf;
        }
    }
}
