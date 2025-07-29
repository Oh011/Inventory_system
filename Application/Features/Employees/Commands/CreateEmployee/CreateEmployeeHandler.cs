using Application.Features.Users.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Project.Application.Features.Employees.Dtos;
using Project.Application.Features.Employees.Interfcaes;
using Project.Application.Features.Users.Dtos;

namespace Project.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponseDto>
    {


        private readonly IUserService userService;


        private readonly IEmployeeService employeeService;

        private readonly IMapper _mapper;



        public CreateEmployeeHandler(IUserService userService, IEmployeeService employeeService, IMapper mapper)
        {
            this.userService = userService;
            this.employeeService = employeeService;
            this._mapper = mapper;

        }

        public async Task<CreateEmployeeResponseDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {



            var userDto = new CreateUserDto
            {
                Email = request.Email
            ,
                FullName = request.FullName,
                Password = request.Password,
                UserName = request.UserName,
                Role = request.Role
            };

            var user = await userService.CreateUser(userDto);





            try
            {

                var employee = _mapper.Map<Employee>(request);
                employee.ApplicationUserId = user.Id;


                var createdEmployee = await employeeService.CreateEmployee(employee);

                var EmployeeResponse = _mapper.Map<CreateEmployeeResponseDto>(createdEmployee);

                EmployeeResponse.Email = user.Email;


                return EmployeeResponse;


            }

            catch (Exception ex)
            {



                await userService.DeleteUserAsync(user.Id);

                throw;


            }







        }
    }
}
