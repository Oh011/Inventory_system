using MediatR;
using Project.Application.Features.Employees.Dtos;

namespace Project.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : UpdateEmployeeRequest, IRequest<CreateEmployeeResponseDto>
    {

        public int Id { get; set; } // EmployeeId to update
    }
}
