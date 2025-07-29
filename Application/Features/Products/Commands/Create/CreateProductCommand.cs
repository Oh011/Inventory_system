using MediatR;
using Microsoft.AspNetCore.Http;
using Project.Application.Features.Products.Dtos;


namespace Project.Application.Features.Products.Commands.Create
{
    public class CreateProductCommand : ProductBaseCommand, IRequest<ProductResultDto>
    {

        public int QuantityInStock { get; set; } = 0;

        public IFormFile? Image { get; set; }


    }
}
