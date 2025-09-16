using AutoMapper;
using MediatR;
using InventorySystem.Application.Features.Categories.Dtos;
using InventorySystem.Application.Features.Categories.Interfaces;

namespace InventorySystem.Application.Features.Categories.Commands.Update
{
    internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryRequest, CategoryDetailsDto>
    {

        private readonly ICategoryService categoryService;

        private readonly IMapper mapper;


        public UpdateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }
        public async Task<CategoryDetailsDto> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {




            var result = await categoryService.UpdateCategory(request);

            return new CategoryDetailsDto
            {

                Id = result.Id,
                Name = result.Name,
                Description = result.Description,

            };


        }
    }
}
