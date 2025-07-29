
using Domain.Entities;
using Project.Application.Common.Parameters;
using Project.Application.Features.Employees.Commands.UpdateEmployee;
using Project.Application.Features.Employees.Commands.UpdateEmployeeImage;
using Project.Application.Features.Employees.Dtos;
using Shared.Results;

namespace Project.Application.Features.Employees.Interfcaes
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
