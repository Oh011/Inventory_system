using AutoMapper;
using Domain.Entities;
using Project.Application.Features.Products.Commands.Create;
using Project.Application.Features.Products.Commands.Update;
using Project.Application.Features.Products.Dtos;

namespace Project.Application.Features.Products.Mapping
{


    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // Map CreateProductCommand → Product entity
            CreateMap<CreateProductCommand, Product>();

            CreateMap<UpdateProductCommand, UpdateProductRequest>().ReverseMap();

            CreateMap<UpdateProductRequest, Product>().ReverseMap()
     .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));




            // Map Product → ProductResultDto
            CreateMap<Product, ProductResultDto>()
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ToString()))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));
        }
    }

}
