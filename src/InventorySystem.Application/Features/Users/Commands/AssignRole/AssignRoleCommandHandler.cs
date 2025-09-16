using Application.Features.Users.Interfaces;
using MediatR;
using InventorySystem.Application.Features.Employees.Interfcaes;

namespace InventorySystem.Application.Features.Users.Commands.AssignRole
{
    internal class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, string>
    {

        private readonly IUserService _userService;

        private readonly IEmployeeService _employeeService;


        public AssignRoleCommandHandler(IUserService userService, IEmployeeService employeeService)
        {

            _userService = userService;


            _employeeService = employeeService;
        }
        public async Task<string> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {

            var result = await _userService.AssignRoleAsync(request.UserId, (request.Role));


            await _employeeService.UpdateEmployeeRoleAsync(request.UserId, result);


            return result;
        }
    }
}
