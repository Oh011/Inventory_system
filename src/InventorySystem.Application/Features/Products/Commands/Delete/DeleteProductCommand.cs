using MediatR;

namespace InventorySystem.Application.Features.Products.Commands.Delete
{
    public record DeleteProductCommand(int Id) : IRequest<Unit>;

}
