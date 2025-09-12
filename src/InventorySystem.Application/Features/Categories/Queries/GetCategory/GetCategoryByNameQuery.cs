using MediatR;
using Project.Application.Features.Categories.Dtos;

namespace Project.Application.Features.Categories.Queries.GetCategory
{
    public class GetCategoryByNameQuery : IRequest<IEnumerable<CategoryDto>>
    {
        public string? Name { get; set; }


        public GetCategoryByNameQuery() { }
        public GetCategoryByNameQuery(string? name = null)
        {
            Name = name;
        }
    }
}
