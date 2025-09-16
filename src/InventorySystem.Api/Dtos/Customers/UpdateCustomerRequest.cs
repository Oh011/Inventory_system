namespace InventorySystem.Api.Dtos.Customers
{
    public class UpdateCustomerRequest
    {

        public string FullName { get; set; } = null!;

        public string Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }
    }
}
