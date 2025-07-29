using MediatR;
using Microsoft.AspNetCore.Http;

namespace Project.Application.Features.Employees.Commands.UpdateEmployeeImage
{
    public class UpdateEmployeeProfileImageCommand : IRequest<string>
    {
        public int EmployeeId { get; set; } // domain ID
        public IFormFile ProfileImage { get; set; } = null!;
    }
}
