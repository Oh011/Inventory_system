using Shared.Errors;

namespace InventorySystem.Application.Common.Validation
{
    // Example specialized error
    public class ProductStockErrorDetail : ValidationErrorDetail
    {
        public int ProductId { get; set; }

        public int RequestedQuantity { get; set; }
        public int AvailableQuantity { get; set; }



        public ProductStockErrorDetail(int productId, int requestedQuantity, int availableQuantity)
            : base($"Insufficient stock for product with Id'{productId}'. Requested: {requestedQuantity}, Available: {availableQuantity}")
        {
            ProductId = productId;

            RequestedQuantity = requestedQuantity;
            AvailableQuantity = availableQuantity;
        }
    }
}
