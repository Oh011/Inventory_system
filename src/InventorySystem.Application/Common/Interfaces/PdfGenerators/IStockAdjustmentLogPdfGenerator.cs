using Project.Application.Features.Inventory.Dtos;

namespace Project.Application.Common.Interfaces.PdfGenerators
{
    public interface IStockAdjustmentLogPdfGenerator
    {
        byte[] Generate(List<StockAdjustmentLogDto> logs);
    }

}
