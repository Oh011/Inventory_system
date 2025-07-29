using MediatR;
using Project.Application.Features.Products.Dtos;

namespace Project.Application.Features.Products.Commands.Update
{
    public class UpdateProductRequest : UpdateProductCommand, IRequest<ProductResultDto>
    {

        public int Id { get; set; }
    }
}
