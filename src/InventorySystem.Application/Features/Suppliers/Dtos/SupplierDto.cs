namespace InventorySystem.Application.Features.Suppliers.Dtos
{

    public class SupplierDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? ContactName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }

}
