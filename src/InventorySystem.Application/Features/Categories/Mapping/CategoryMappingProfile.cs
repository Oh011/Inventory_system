using AutoMapper;
using Domain.Entities;
using InventorySystem.Application.Features.Categories.Commands.Create;
using InventorySystem.Application.Features.Categories.Dtos;

namespace InventorySystem.Application.Features.Categories.Mapping
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
