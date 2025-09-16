using AutoMapper;
using MediatR;
using InventorySystem.Application.Features.Employees.Dtos;
using InventorySystem.Application.Features.Employees.Interfcaes;

namespace InventorySystem.Application.Features.Employees.Commands.UpdateEmployee
{
    internal class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, CreateEmployeeResponseDto>
    {

        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;



        public UpdateEmployeeCommandHandler(IEmployeeService employeeService, IMapper mapper)
        {
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        public async Task<CreateEmployeeResponseDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {


            var employee = await employeeService.UpdateEmployee(request);




            return mapper.Map<CreateEmployeeResponseDto>(employee);
        }
    }
}
