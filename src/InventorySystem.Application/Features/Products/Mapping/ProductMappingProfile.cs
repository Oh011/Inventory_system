using AutoMapper;
using Domain.Entities;
using InventorySystem.Application.Features.Products.Commands.Create;
using InventorySystem.Application.Features.Products.Commands.Update;
using InventorySystem.Application.Features.Products.Dtos;

namespace InventorySystem.Application.Features.Products.Mapping
{


    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // Map CreateProductCommand → Product entity
            CreateMap<CreateProductCommand, Product>();


            CreateMap<UpdateProductRequest, UpdateProductCommand>().ReverseMap();

            CreateMap<UpdateProductCommand, Product>().ReverseMap()
     .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));




            // Map Product → ProductResultDto
            CreateMap<Product, ProductResultDto>()
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ToString()))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));
        }
    }

}
