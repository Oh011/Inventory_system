using AutoMapper;
using Project.Application.Features.Inventory.Commands.AdjustInventory;
using Project.Application.Features.Inventory.Dtos;

namespace Project.Application.Features.Inventory.Mapping
{
    public class InventoryMappingProfile : Profile
    {



        public InventoryMappingProfile()
        {



            CreateMap<AdjustInventoryCommand, AdjustInventoryLogDto>().ReverseMap();

        }
    }
}
