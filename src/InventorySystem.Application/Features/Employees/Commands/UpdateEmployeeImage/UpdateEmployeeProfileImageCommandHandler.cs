using MediatR;
using Project.Application.Common.Interfaces.Services;
using Project.Application.Features.Employees.Interfcaes;

namespace Project.Application.Features.Employees.Commands.UpdateEmployeeImage
{
    internal class UpdateEmployeeProfileImageCommandHandler : IRequestHandler<UpdateEmployeeProfileImageCommand, string>
    {

        private readonly IEmployeeService employeeService;
        private readonly IUriService uriService;



        public UpdateEmployeeProfileImageCommandHandler(IEmployeeService employeeService, IUriService uriService)
        {

            this.uriService = uriService;
            this.employeeService = employeeService;
        }
        public async Task<string> Handle(UpdateEmployeeProfileImageCommand request, CancellationToken cancellationToken)
        {

            var imagePath = await employeeService.UpdateEmployeeProfileImage(request);



            return uriService.GetAbsoluteUri(imagePath);


        }
    }
}
