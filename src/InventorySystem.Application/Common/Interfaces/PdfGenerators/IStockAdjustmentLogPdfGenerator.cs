using InventorySystem.Application.Features.Inventory.Dtos;

namespace InventorySystem.Application.Common.Interfaces.PdfGenerators
{
    public interface IStockAdjustmentLogPdfGenerator
    {
        byte[] Generate(List<StockAdjustmentLogDto> logs);
    }

}
