using MediatR;
using Project.Application.Features.Categories.Dtos;

namespace Project.Application.Features.Categories.Queries.CategoryLookUp
{
    public class GetCategoryLookUpQuery : IRequest<IEnumerable<CategoryDto>>
    {
    }
}
