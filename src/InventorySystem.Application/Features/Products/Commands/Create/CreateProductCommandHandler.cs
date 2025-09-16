using AutoMapper;
using Domain.Entities;
using MediatR;
using InventorySystem.Application.Features.Products.Dtos;
using InventorySystem.Application.Features.Products.Intrefaces;

namespace InventorySystem.Application.Features.Products.Commands.Create
{
    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResultDto>
    {


        private readonly IProductService productService;

        private readonly IMapper mapper;


        public CreateProductCommandHandler(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        public async Task<ProductResultDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {


            var product = mapper.Map<Product>(request);

            var createdProduct = await productService.CreateProduct(product, request.Image);

            var resultDto = mapper.Map<ProductResultDto>(createdProduct);

            return resultDto;

        }
    }
}
