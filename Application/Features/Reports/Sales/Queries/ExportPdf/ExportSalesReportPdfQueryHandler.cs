using MediatR;
using Project.Application.Common.Interfaces.PdfGenerators;
using Project.Application.Features.Reports.Sales.Interfaces;

namespace Project.Application.Features.Reports.Sales.Queries.ExportPdf
{
    internal class ExportSalesReportPdfQueryHandler : IRequestHandler<ExportSalesReportPdfQuery, byte[]>
    {

        private readonly ISalesReportService _reportService;
        private readonly ISalesReportPdfGenerator _pdfGenerator;

        public ExportSalesReportPdfQueryHandler(ISalesReportService reportService, ISalesReportPdfGenerator pdfGenerator)
        {
            _reportService = reportService;
            _pdfGenerator = pdfGenerator;
        }

        public async Task<byte[]> Handle(ExportSalesReportPdfQuery request, CancellationToken cancellationToken)
        {
            var reportData = await _reportService.GenerateReportAsync(request, cancellationToken);

            var pdf = _pdfGenerator.GenerateSalesReportPdf(reportData, request.FromDate, request.ToDate); ;
            return pdf;
        }
    }
}
