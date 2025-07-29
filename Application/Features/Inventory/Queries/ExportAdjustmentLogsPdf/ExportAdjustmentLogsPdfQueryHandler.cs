using Domain.Entities;
using MediatR;
using Project.Application.Common.Interfaces.PdfGenerators;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Features.Inventory.Specifications;

namespace Project.Application.Features.Inventory.Queries.ExportAdjustmentLogsPdf
{
    internal class ExportAdjustmentLogsPdfQueryHandler : IRequestHandler<ExportAdjustmentLogsPdfQuery, byte[]>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IStockAdjustmentLogPdfGenerator pdfGenerator;

        public ExportAdjustmentLogsPdfQueryHandler(
            IUnitOfWork unitOfWork,
            IStockAdjustmentLogPdfGenerator pdfGenerator)
        {
            this.unitOfWork = unitOfWork;
            this.pdfGenerator = pdfGenerator;
        }

        public async Task<byte[]> Handle(ExportAdjustmentLogsPdfQuery request, CancellationToken cancellationToken)
        {
            var repository = unitOfWork.GetRepository<StockAdjustmentLog, int>();
            var specifications = new GetAdjustmentLogsSpecifications(request);

            var logs = await repository.GetAllWithProjectionSpecifications(specifications);

            return pdfGenerator.Generate(logs.ToList());
        }
    }


}
