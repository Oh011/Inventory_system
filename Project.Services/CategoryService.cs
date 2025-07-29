
using Application.Exceptions;
using Application.Specifications.CategorySpec;
using Domain.Entities;
using Project.Application.Common.Interfaces.Repositories;
using Project.Application.Features.Categories.Commands.Update;
using Project.Application.Features.Categories.Dtos;
using Project.Application.Features.Categories.Interfaces;

namespace Project.Services
{
    class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
    {
        public async Task<Category> CreateCategory(Category category)
        {

            var repository = unitOfWork.GetRepository<Category, int>();

            await repository.AddAsync(category);

            await unitOfWork.Commit();

            return category;


        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories(string? Name)
        {


            var repository = unitOfWork.GetRepository<Category, int>();

            var categorySpecifications = new CategoryByNameSpecification(Name);

            var categories = await repository.ListAsync(categorySpecifications, c => new CategoryDto { Id = c.Id, Name = c.Name });


            return categories;
        }

        public async Task<Category?> GetCategoryById(int Id)
        {


            var repository = unitOfWork.GetRepository<Category, int>();


            var category = await repository.GetById(Id);


            if (category == null)
            {

                throw new NotFoundException($"Category with Id :{Id} is not found");
            }


            return category;

        }


        public async void CheckCategoriesExist(IEnumerable<int> categoryIds)
        {


            var categoryRepository = unitOfWork.GetRepository<Category, int>();

            var existingCategoryIds = await (categoryRepository
                                  .ListAsync(c => categoryIds.Contains(c.Id), c => c.Id));



            var missingCategoryIds = categoryIds.Except(existingCategoryIds).ToList();

            if (missingCategoryIds.Any())
                throw new NotFoundException($"Category IDs not found: {string.Join(", ", missingCategoryIds)}");
        }

        public async Task<Category> UpdateCategory(UpdateCategoryRequest category)
        {


            var categoryRepository = unitOfWork.GetRepository<Category, int>();

            var _category = await GetCategoryById(category.Id);




            _category.Name = category.Name;

            if (category.Description != null)
                _category.Description = category.Description;


            categoryRepository.Update(_category);

            await unitOfWork.Commit();


            return _category;

        }
    }
}
