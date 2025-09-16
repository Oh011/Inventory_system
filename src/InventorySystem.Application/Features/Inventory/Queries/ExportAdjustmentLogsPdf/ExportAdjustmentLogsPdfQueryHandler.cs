using Domain.Entities;
using MediatR;
using InventorySystem.Application.Common.Interfaces.PdfGenerators;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Inventory.Specifications;

namespace InventorySystem.Application.Features.Inventory.Queries.ExportAdjustmentLogsPdf
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
