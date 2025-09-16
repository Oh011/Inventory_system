using MediatR;
using InventorySystem.Application.Features.Categories.Dtos;

namespace InventorySystem.Application.Features.Categories.Queries.GetCategory
{
    public class GetCategoryByIdQuery : IRequest<CategoryDetailsDto?>
    {
        public int Id { get; set; }
        public GetCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
