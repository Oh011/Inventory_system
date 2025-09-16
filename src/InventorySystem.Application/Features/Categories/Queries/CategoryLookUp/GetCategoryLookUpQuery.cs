using MediatR;
using InventorySystem.Application.Features.Categories.Dtos;

namespace InventorySystem.Application.Features.Categories.Queries.CategoryLookUp
{
    public class GetCategoryLookUpQuery : IRequest<IEnumerable<CategoryDto>>
    {
    }
}
