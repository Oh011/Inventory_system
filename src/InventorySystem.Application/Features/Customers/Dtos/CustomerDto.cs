namespace InventorySystem.Application.Features.Customers.Dtos
{
    public class CustomerDto
    {


        public int Id { get; set; }
        public string FullName { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }
    }
}
