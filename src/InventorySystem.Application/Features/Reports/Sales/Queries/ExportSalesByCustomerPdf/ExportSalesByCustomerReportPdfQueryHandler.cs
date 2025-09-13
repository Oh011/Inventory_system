using Domain.Entities;
using InventorySystem.Application.Features.Reports.Sales.Specifications;
using MediatR;
using Project.Application.Common.Interfaces.PdfGenerators;
using Project.Application.Common.Interfaces.Repositories;

namespace InventorySystem.Application.Features.Reports.Sales.Queries.ExportSalesByCustomerPdf
{
    internal class ExportSalesByCustomerReportPdfQueryHandler : IRequestHandler<ExportSalesByCustomerReportPdfQuery, byte[]>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly ISalesReportPdfGenerator pdfGenerator;


        public ExportSalesByCustomerReportPdfQueryHandler(IUnitOfWork unitOfWork, ISalesReportPdfGenerator pdfGenerator)
        {

            this.pdfGenerator = pdfGenerator;
            this.unitOfWork = unitOfWork;
        }

        public async Task<byte[]> Handle(ExportSalesByCustomerReportPdfQuery request, CancellationToken cancellationToken)
        {

            var specifications = new SalesByCustomerReportSpecification(request);

            var repository = unitOfWork.GetRepository<SalesInvoice, int>();

            var result = await repository.GetAllWithGrouping(specifications);

            var pdf = pdfGenerator.GenerateSalesByCustomerReportPdf(result, request.FromDate, request.ToDate);


            return pdf;
        }
    }
}
