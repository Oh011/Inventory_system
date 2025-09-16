namespace InventorySystem.Application.Features.Categories.Dtos
{
    public class CategoryDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
