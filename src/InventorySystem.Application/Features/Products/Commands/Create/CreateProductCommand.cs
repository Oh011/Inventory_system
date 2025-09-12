using MediatR;
using Project.Application.Features.Products.Dtos;
using Shared.Dtos;


namespace Project.Application.Features.Products.Commands.Create
{
    public class CreateProductCommand : ProductBaseCommand, IRequest<ProductResultDto>
    {

        public int QuantityInStock { get; set; } = 0;

        public FileUploadDto? Image { get; set; }


    }
}
