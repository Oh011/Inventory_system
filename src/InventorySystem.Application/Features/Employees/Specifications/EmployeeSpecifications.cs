using Domain.Entities;
using Domain.Specifications;
using Project.Application.Common.Enums.SortOptions;
using Project.Application.Common.Parameters;
using Project.Application.Features.Employees.Dtos;

namespace Project.Application.Features.Employees.Specifications
{
    public class EmployeeSpecifications : ProjectionSpecifications<Employee, EmployeeSummaryDto>
    {


        public EmployeeSpecifications(EmployeeFilterParams filter)
                    : base(e =>
                        (string.IsNullOrEmpty(filter.Name) || e.FullName.ToLower().Contains(filter.Name.ToLower())) &&
                        (string.IsNullOrEmpty(filter.JobTitle) || e.JobTitle.ToLower().Contains(filter.JobTitle.ToLower())) &&
                        (string.IsNullOrEmpty(filter.Role) || e.Role.ToLower() == filter.Role.ToLower()) &&
                        (!filter.IsActive.HasValue || e.IsActive == filter.IsActive)
                    )
        {




            if (filter.EmployeeSortOptions != null)
            {
                switch (filter.EmployeeSortOptions)
                {
                    case EmployeeSortOptions.FullNameAsc:
                        SetOrderBy(e => e.FullName);
                        break;
                    case EmployeeSortOptions.FullNameDesc:
                        SetOrderByDescending(e => e.FullName);
                        break;
                    case EmployeeSortOptions.CreatedAtAsc:
                        SetOrderBy(e => e.CreatedAt);
                        break;
                    case EmployeeSortOptions.CreatedAtDesc:
                        SetOrderByDescending(e => e.CreatedAt);
                        break;
                    case EmployeeSortOptions.JobTitleAsc:
                        SetOrderBy(e => e.JobTitle);
                        break;
                    case EmployeeSortOptions.JobTitleDesc:
                        SetOrderByDescending(e => e.JobTitle);
                        break;

                    default:
                        SetOrderByDescending(e => e.CreatedAt);
                        break;
                }

            }

            else
            {
                SetOrderByDescending(e => e.CreatedAt);
            }




            AddProjection(e => new EmployeeSummaryDto
            {
                EmployeeId = e.Id,
                UserId = e.ApplicationUserId,
                FullName = e.FullName,
                Email = "", // To be included via JOIN if needed from Identity or injected elsewhere
                Role = e.Role,
                JobTitle = e.JobTitle,
                IsActive = e.IsActive,
                ProfileImageUrl = e.ProfileImageUrl,
                CreatedAt = e.CreatedAt
            });


            ApplyPagination(filter.PageIndex, filter.pageSize);

        }
    }
}
