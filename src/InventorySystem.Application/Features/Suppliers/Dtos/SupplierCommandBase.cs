namespace InventorySystem.Application.Features.Suppliers.Dtos
{
    public class SupplierCommandBase
    {
        public string Name { get; set; } = default!;
        public string? ContactName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public List<int>? CategoryIds { get; set; }
    }

}
