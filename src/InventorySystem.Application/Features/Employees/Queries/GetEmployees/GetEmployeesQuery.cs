using MediatR;
using InventorySystem.Application.Common.Parameters;
using InventorySystem.Application.Features.Employees.Dtos;
using Shared.Results;

namespace InventorySystem.Application.Features.Employees.Queries.GetEmployees
{
    public class GetEmployeesQuery : IRequest<PaginatedResult<EmployeeSummaryDto>>
    {

        public EmployeeFilterParams Filter { get; set; }


        public GetEmployeesQuery(EmployeeFilterParams filter)
        {
            this.Filter = filter;
        }
    }
}
