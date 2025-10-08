using InventorySystem.Domain.Enums;

namespace InventorySystem.Application.Features.SalesReturn.Dtos
{


    public class CreateSalesReturnItemDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; } // captured from SalesInvoiceItem at time of return

        public ReturnCondition Condition { get; set; }
    }
}
