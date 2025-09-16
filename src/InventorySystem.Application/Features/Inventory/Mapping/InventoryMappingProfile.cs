using AutoMapper;
using InventorySystem.Application.Features.Inventory.Commands.AdjustInventory;
using InventorySystem.Application.Features.Inventory.Dtos;

namespace InventorySystem.Application.Features.Inventory.Mapping
{
    public class InventoryMappingProfile : Profile
    {



        public InventoryMappingProfile()
        {



            CreateMap<AdjustInventoryCommand, AdjustInventoryLogDto>().ReverseMap();

        }
    }
}
