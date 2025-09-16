using MediatR;
using InventorySystem.Application.Features.Employees.Dtos;

namespace InventorySystem.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : UpdateEmployeeRequest, IRequest<CreateEmployeeResponseDto>
    {

        public int Id { get; set; } // EmployeeId to update
    }
}
