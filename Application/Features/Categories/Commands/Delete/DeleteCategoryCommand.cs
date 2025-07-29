using MediatR;

namespace Project.Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest<Unit>
    {

        public int Id { get; set; }


        public DeleteCategoryCommand(int id) { this.Id = id; }
    }
}
