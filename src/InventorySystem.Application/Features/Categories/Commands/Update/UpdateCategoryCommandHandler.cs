using AutoMapper;
using MediatR;
using Project.Application.Features.Categories.Dtos;
using Project.Application.Features.Categories.Interfaces;

namespace Project.Application.Features.Categories.Commands.Update
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
