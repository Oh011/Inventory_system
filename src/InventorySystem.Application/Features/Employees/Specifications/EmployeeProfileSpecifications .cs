using Domain.Entities;
using Domain.Specifications;
using InventorySystem.Application.Features.Employees.Dtos;

namespace InventorySystem.Application.Features.Employees.Specifications
{
    internal class EmployeeProfileSpecifications : ProjectionSpecifications<Employee, EmployeeProfileDto>
    {




        public EmployeeProfileSpecifications(string? userId = null, int? employeeId = null)
       : base(e =>
           (userId != null && e.ApplicationUserId == userId) ||
           (employeeId != null && e.Id == employeeId))
        {




            AddProjection(e => new EmployeeProfileDto
            {

                EmployeeId = e.Id,
                UserId = e.ApplicationUserId,
                FullName = e.FullName,
                Email = "",
                Role = e.Role, // or via UserManager
                JobTitle = e.JobTitle,
                Address = e.Address,
                NationalId = e.NationalId,
                IsActive = e.IsActive,
                ProfileImageUrl = e.ProfileImageUrl,
                CreatedAt = e.CreatedAt
            });
        }


    }
}
