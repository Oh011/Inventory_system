using AutoMapper;
using Domain.Entities;
using InventorySystem.Application.Features.PurchaseOrders.Dtos;

namespace InventorySystem.Application.Features.PurchaseOrders.Mapping
{
    internal class PurchaseOrderMappingProfile : Profile
    {


        public PurchaseOrderMappingProfile()
        {


            CreateMap<PurchaseOrder, PurchaseOrderDetailDto>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())
                )
                 .ReverseMap();


            CreateMap<PurchaseOrderItem, PurchaseOrderItemDto>().ReverseMap();

        }
    }
}
