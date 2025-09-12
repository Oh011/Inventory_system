
using Domain.Entities;
using Project.Application.Features.Categories.Commands.Update;
using Project.Application.Features.Categories.Dtos;

namespace Project.Application.Features.Categories.Interfaces
{
    public interface ICategoryService
    {


        Task<Category> CreateCategory(Category category);

        public void CheckCategoriesExist(IEnumerable<int> categoryIds);
        Task<IEnumerable<CategoryDto>> GetAllCategories(string? Name = null);


        Task<Category> UpdateCategory(UpdateCategoryRequest category);

        Task<Category?> GetCategoryById(int Id);
    }
}
