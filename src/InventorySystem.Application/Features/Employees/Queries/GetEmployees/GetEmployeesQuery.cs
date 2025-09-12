using MediatR;
using Project.Application.Common.Parameters;
using Project.Application.Features.Employees.Dtos;
using Shared.Results;

namespace Project.Application.Features.Employees.Queries.GetEmployees
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
