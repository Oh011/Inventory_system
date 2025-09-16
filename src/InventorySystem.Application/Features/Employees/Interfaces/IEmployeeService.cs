
using Domain.Entities;
using InventorySystem.Application.Common.Parameters;
using InventorySystem.Application.Features.Employees.Commands.UpdateEmployee;
using InventorySystem.Application.Features.Employees.Commands.UpdateEmployeeImage;
using InventorySystem.Application.Features.Employees.Dtos;
using Shared.Results;

namespace InventorySystem.Application.Features.Employees.Interfcaes
{
    public interface IEmployeeService
    {


        Task<string?> UpdateEmployeeProfileImage(UpdateEmployeeProfileImageCommand request);
        Task UpdateEmployeeRoleAsync(string userId, string newRole);


        Task<PaginatedResult<EmployeeSummaryDto>> GetAllEmployees(EmployeeFilterParams filters);

        Task<Employee> UpdateEmployee(UpdateEmployeeCommand employee);
        Task<Employee> CreateEmployee(Employee employee);
    }
}
