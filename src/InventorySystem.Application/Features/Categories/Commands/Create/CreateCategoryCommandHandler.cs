using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Categories.Dtos;
using Project.Application.Features.Categories.Interfaces;

namespace Project.Application.Features.Categories.Commands.Create
{
    internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {


        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;


        public CreateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }
        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {


            var category = _mapper.Map<Category>(request);


            var result = await _categoryService.CreateCategory(category);


            return _mapper.Map<CategoryDto>(result);

        }
    }
}
