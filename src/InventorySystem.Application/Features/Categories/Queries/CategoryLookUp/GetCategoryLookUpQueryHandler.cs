using MediatR;
using InventorySystem.Application.Features.Categories.Dtos;
using InventorySystem.Application.Features.Categories.Interfaces;

namespace InventorySystem.Application.Features.Categories.Queries.CategoryLookUp
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
