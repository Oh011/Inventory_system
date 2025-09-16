using MediatR;
using InventorySystem.Application.Features.Products.Dtos;
using Shared.Dtos;


namespace InventorySystem.Application.Features.Products.Commands.Create
{
    public class CreateProductCommand : ProductBaseCommand, IRequest<ProductResultDto>
    {

        public int QuantityInStock { get; set; } = 0;

        public FileUploadDto? Image { get; set; }


    }
}
