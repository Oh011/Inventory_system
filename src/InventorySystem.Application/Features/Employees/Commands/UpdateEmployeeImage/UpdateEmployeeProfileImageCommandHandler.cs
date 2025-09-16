using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using MediatR;
using InventorySystem.Application.Features.Employees.Interfcaes;

namespace InventorySystem.Application.Features.Employees.Commands.UpdateEmployeeImage
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
