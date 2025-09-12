using MediatR;
using Shared.Dtos;

namespace Project.Application.Features.Employees.Commands.UpdateEmployeeImage
{
    public class UpdateEmployeeProfileImageCommand : IRequest<string>
    {
        public int EmployeeId { get; set; } // domain ID
        public FileUploadDto? ProfileImage { get; set; }
    }
}
