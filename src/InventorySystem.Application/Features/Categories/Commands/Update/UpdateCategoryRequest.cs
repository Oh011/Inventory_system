using MediatR;
using Project.Application.Features.Categories.Dtos;

namespace Project.Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryRequest : UpdateCategoryCommand, IRequest<CategoryDetailsDto>
    {


        public int Id { get; set; }
    }
}
