using AutoMapper;
using Domain.Entities;
using Project.Application.Features.Employees.Commands.CreateEmployee;
using Project.Application.Features.Employees.Commands.UpdateEmployee;
using Project.Application.Features.Employees.Dtos;

namespace Project.Application.Features.Employees.Mapping
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
