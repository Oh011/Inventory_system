using AutoMapper;
using Domain.Entities;
using InventorySystem.Application.Features.Employees.Commands.CreateEmployee;
using InventorySystem.Application.Features.Employees.Commands.UpdateEmployee;
using InventorySystem.Application.Features.Employees.Dtos;

namespace InventorySystem.Application.Features.Employees.Mapping
{
    internal class EmployeeMappingProfile : Profile
    {



        public EmployeeMappingProfile()
        {


            CreateMap<CreateEmployeeCommand, Employee>().ReverseMap();

            CreateMap<UpdateEmployeeCommand, UpdateEmployeeRequest>().ReverseMap();

            CreateMap<Employee, CreateEmployeeResponseDto>().ForMember(

                dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id)


                ).ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ApplicationUserId)).ReverseMap();


        }
    }
}
