namespace InventorySystem.Application.Features.PurchaseOrders.Dtos
{
    public class UpdatePurchaseOrderRequest
    {



        public string RowVersion { get; set; } = string.Empty;

        public List<UpdatePurchaseOrderItemDto> Items { get; set; } = new();


        public UpdatePurchaseOrderRequest(string rowVersion, List<UpdatePurchaseOrderItemDto> Items)
        {


            this.RowVersion = rowVersion;
            this.Items = Items;
        }



    }
}
