using AutoMapper;
using MediatR;
using InventorySystem.Application.Features.Products.Dtos;
using InventorySystem.Application.Features.Products.Intrefaces;

namespace InventorySystem.Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResultDto>
    {


        private readonly IProductService productService;
        private readonly IMapper _mapper;



        public UpdateProductCommandHandler(IProductService productService, IMapper mapper)
        {
            this._mapper = mapper;
            this.productService = productService;
        }
        public async Task<ProductResultDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {


            var product = await productService.UpdateProduct(request);



            return _mapper.Map<ProductResultDto>(product);

        }
    }
}
