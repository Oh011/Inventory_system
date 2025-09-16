using Domain.Entities;
using InventorySystem.Application.Features.Reports.Sales.Specifications;
using MediatR;
using InventorySystem.Application.Common.Interfaces.PdfGenerators;
using InventorySystem.Application.Common.Interfaces.Repositories;

namespace InventorySystem.Application.Features.Reports.Sales.Queries.ExportSalesByCategoryPdf
{
    internal class ExportSalesByCategoryReportPdfQueryHandler : IRequestHandler<ExportSalesByCategoryReportPdfQuery, byte[]>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly ISalesReportPdfGenerator pdfGenerator;


        public ExportSalesByCategoryReportPdfQueryHandler(IUnitOfWork unitOfWork, ISalesReportPdfGenerator salesReportPdfGenerator)
        {
            this.unitOfWork = unitOfWork;
            this.pdfGenerator = salesReportPdfGenerator;
        }



        public async Task<byte[]> Handle(ExportSalesByCategoryReportPdfQuery request, CancellationToken cancellationToken)
        {


            var specifications = new SalesByCategoryReportSpecification(request);

            var repository = unitOfWork.GetRepository<SalesInvoiceItem, int>();

            var result = await repository.GetAllWithGrouping(specifications);


            var pdf = pdfGenerator.GenerateSalesByCategoryReportPdf(result,
                request.FromDate, request.ToDate);


            return pdf;
        }
    }
}
