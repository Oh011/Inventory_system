using MediatR;
using InventorySystem.Application.Features.Categories.Dtos;

namespace InventorySystem.Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommand : IRequest<CategoryDto>  // Return created category ID
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}
