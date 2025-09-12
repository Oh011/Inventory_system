using AutoMapper;
using Domain.Entities;
using Project.Application.Features.Customers.Dtos;

namespace Project.Application.Features.Customers.Mapping
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
