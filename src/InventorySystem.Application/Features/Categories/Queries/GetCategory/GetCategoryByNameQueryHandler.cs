using MediatR;
using Project.Application.Features.Categories.Dtos;
using Project.Application.Features.Categories.Interfaces;

namespace Project.Application.Features.Categories.Queries.GetCategory
{
    internal class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, IEnumerable<CategoryDto>>
    {

        private readonly ICategoryService categoryService;


        public GetCategoryByNameQueryHandler(ICategoryService categoryService)
        {

            this.categoryService = categoryService;
        }
        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {


            var categories = await categoryService.GetAllCategories(request.Name);


            return categories;
        }
    }
}
