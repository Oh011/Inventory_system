using MediatR;
using InventorySystem.Application.Features.Products.Dtos;

namespace InventorySystem.Application.Features.Products.Commands.Update
{
    public class UpdateProductCommand : UpdateProductRequest, IRequest<ProductResultDto>
    {

        public int Id { get; set; }
    }
}
