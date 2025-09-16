
using Domain.Enums;
using InventorySystem.Application.Common.Dtos;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Categories.Interfaces;
using InventorySystem.Application.Features.Products.Dtos;
using MediatR;

namespace InventorySystem.Application.Features.Products.queries.ProductCreationData
{
    internal class GetProductCreationDataQueryHandler : IRequestHandler<GetProductCreationDataQuery, ProductCreationDataDto>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly ICategoryService categoryService;


        public GetProductCreationDataQueryHandler(IUnitOfWork unitOfWork, ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ProductCreationDataDto> Handle(GetProductCreationDataQuery request, CancellationToken cancellationToken)
        {

            var categories = (await categoryService.GetAllCategories(null)).ToList();



            var unitTypes = Enum.GetValues(typeof(UnitType))
           .Cast<UnitType>()
           .Select(u => new EnumDto
           {
               Name = u.ToString(),
               Value = (int)u
           })
           .ToList();


            return new ProductCreationDataDto
            {

                Categories = categories,
                UnitTypes = unitTypes
            };


        }
    }
}
