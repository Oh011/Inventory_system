namespace InventorySystem.Application.Features.SalesInvoice.Dtos
{
    public class SalesInvoiceItemWithReturnInfoDto : SalesInvoiceItemDto
    {

        public int ReturnedQuantity { get; set; }
        public int AvailableForReturn => QuantitySold - ReturnedQuantity;

    }
}
