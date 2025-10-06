using InventorySystem.Application.Features.Employees.Dtos;
using MediatR;
using Shared.Results;

namespace InventorySystem.Application.Features.Employees.Queries.GetEmployeeProfile
{
    public record GetEmployeeProfileQuery(string userId) : IRequest<Result<EmployeeProfileDto>>;

}
