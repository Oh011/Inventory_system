using MediatR;
using InventorySystem.Application.Features.Employees.Dtos;

namespace InventorySystem.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<CreateEmployeeResponseDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }


        public string FullName { get; set; }
        public string Role { get; set; }
        public string? JobTitle { get; set; }
        public string? Address { get; set; }
        public string? NationalId { get; set; }
    }
}
