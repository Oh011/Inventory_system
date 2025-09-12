using Project.Application.Common.Enums.SortOptions;
using Shared.Parameters;

namespace Project.Application.Common.Parameters
{
    public class EmployeeFilterParams : PaginationQueryParameters
    {

        public string? Name { get; set; }
        public string? JobTitle { get; set; }
        public string? Role { get; set; }
        public bool? IsActive { get; set; }


        public EmployeeSortOptions? EmployeeSortOptions { get; set; }
    }
}
