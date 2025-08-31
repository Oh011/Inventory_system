using MediatR;
using Project.Application.Features.Categories.Dtos;
using Project.Application.Features.Categories.Interfaces;

namespace Project.Application.Features.Categories.Queries.CategoryLookUp
{
    internal class GetCategoryLookUpQueryHandler : IRequestHandler<GetCategoryLookUpQuery, IEnumerable<CategoryDto>>
    {



        private readonly ICategoryService _categoryService;


        public GetCategoryLookUpQueryHandler(ICategoryService categoryService)
        {

            _categoryService = categoryService;
        }
        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoryLookUpQuery request, CancellationToken cancellationToken)
        {


            var categories = (await _categoryService.GetAllCategories()).ToList();
            return categories;

        }
    }
}
