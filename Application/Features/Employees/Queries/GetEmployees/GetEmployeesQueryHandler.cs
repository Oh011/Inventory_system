using MediatR;
using Project.Application.Features.Employees.Dtos;
using Project.Application.Features.Employees.Interfcaes;
using Shared.Results;

namespace Project.Application.Features.Employees.Queries.GetEmployees
{
    internal class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, PaginatedResult<EmployeeSummaryDto>>
    {

        private readonly IEmployeeService _employeeService;


        public GetEmployeesQueryHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<PaginatedResult<EmployeeSummaryDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetAllEmployees(request.Filter);


            return employees;
        }
    }
}
