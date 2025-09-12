using MediatR;
using Project.Application.Features.Categories.Dtos;

namespace Project.Application.Features.Categories.Queries.GetCategory
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
