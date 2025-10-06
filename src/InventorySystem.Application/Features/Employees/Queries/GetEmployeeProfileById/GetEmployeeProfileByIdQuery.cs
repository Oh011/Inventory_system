using InventorySystem.Application.Features.Employees.Dtos;
using MediatR;
using Shared.Results;

namespace InventorySystem.Application.Features.Employees.Queries.GetEmployeeProfileById
{
    public record GetEmployeeProfileByIdQuery(int employeeId) : IRequest<Result<EmployeeProfileDto>>;

}
