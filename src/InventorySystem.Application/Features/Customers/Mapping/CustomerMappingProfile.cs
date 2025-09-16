using AutoMapper;
using Domain.Entities;
using InventorySystem.Application.Features.Customers.Dtos;

namespace InventorySystem.Application.Features.Customers.Mapping
{
    public class CustomerMappingProfile : Profile
    {


        public CustomerMappingProfile()
        {


            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}
