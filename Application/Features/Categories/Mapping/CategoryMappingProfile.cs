using AutoMapper;
using Domain.Entities;
using Project.Application.Features.Categories.Commands.Create;
using Project.Application.Features.Categories.Dtos;

namespace Project.Application.Features.Categories.Mapping
{
    internal class CategoryMappingProfile : Profile
    {


        public CategoryMappingProfile()
        {


            CreateMap<CreateCategoryCommand, Category>();

            // Category -> CategoryDto (for returning to client)
            CreateMap<Category, CategoryDto>();

            CreateMap<Category, CategoryDetailsDto>().ReverseMap();

        }
    }
}
