using AutoMapper;
using MediatR;
using Project.Application.Features.Categories.Dtos;
using Project.Application.Features.Categories.Interfaces;

namespace Project.Application.Features.Categories.Queries.GetCategory
{
    internal class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDetailsDto?>
    {

        private readonly ICategoryService _categoryService;

        private readonly IMapper mapper;

        public GetCategoryByIdQueryHandler(ICategoryService categoryService, IMapper mapper)
        {

            this.mapper = mapper;
            this._categoryService = categoryService;
        }
        public async Task<CategoryDetailsDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {



            var category = await _categoryService.GetCategoryById(request.Id);



            return mapper.Map<CategoryDetailsDto>(category);
        }
    }
}
