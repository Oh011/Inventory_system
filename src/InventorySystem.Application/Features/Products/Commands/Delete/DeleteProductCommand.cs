using MediatR;

namespace Project.Application.Features.Products.Commands.Delete
{
    public record DeleteProductCommand(int Id) : IRequest<Unit>;

}
