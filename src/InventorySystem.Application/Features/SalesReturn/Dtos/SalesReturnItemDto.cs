using InventorySystem.Domain.Enums;

namespace InventorySystem.Application.Features.SalesReturn.Dtos
{
    public class SalesReturnItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public ReturnCondition Condition { get; set; }
    }
}
