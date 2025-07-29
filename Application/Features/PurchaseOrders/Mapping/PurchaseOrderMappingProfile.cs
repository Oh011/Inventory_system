using AutoMapper;
using Domain.Entities;
using Project.Application.Features.PurchaseOrders.Dtos;

namespace Project.Application.Features.PurchaseOrders.Mapping
{
    internal class PurchaseOrderMappingProfile : Profile
    {


        public PurchaseOrderMappingProfile()
        {


            CreateMap<PurchaseOrder, PurchaseOrderResultDto>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())
                )
                 .ReverseMap();


            CreateMap<PurchaseOrderItem, PurchaseOrderItemDto>().ReverseMap();

        }
    }
}
