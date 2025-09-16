namespace InventorySystem.Application.Features.Suppliers.Mapping
{
    using AutoMapper;
    using Domain.Entities;
    using InventorySystem.Application.Features.Suppliers.Commands.Update;
    using InventorySystem.Application.Features.Suppliers.Dtos;

    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierDto>();

            CreateMap<UpdateSupplierCommand, UpdateSupplierRequest>().ReverseMap();

            // If needed, map nested collections like SupplierCategories to CategoryDtos, etc.
        }
    }

}
