using MediatR;
using InventorySystem.Application.Features.Categories.Dtos;

namespace InventorySystem.Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryRequest : UpdateCategoryCommand, IRequest<CategoryDetailsDto>
    {


        public int Id { get; set; }
    }
}
