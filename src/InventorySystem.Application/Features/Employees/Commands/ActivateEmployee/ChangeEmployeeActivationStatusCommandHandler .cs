using Application.Exceptions;
using Application.Features.Users.Interfaces;
using Domain.Entities;
using InventorySystem.Application.Common.Interfaces.Services.Interfaces;
using MediatR;
using InventorySystem.Application.Common.Interfaces.Repositories;

namespace InventorySystem.Application.Features.Employees.Commands.ActivateEmployee
{
    internal class ChangeEmployeeActivationStatusCommandHandler : IRequestHandler<ChangeEmployeeActivationStatusCommand, string>
    {


        private readonly IUserService userService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAuthorizationService authorizationService;

        public ChangeEmployeeActivationStatusCommandHandler(IUserService userService, IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
        {

            this.userService = userService;
            this.unitOfWork = unitOfWork;
            this.authorizationService = authorizationService;
        }

        public async Task<string> Handle(ChangeEmployeeActivationStatusCommand request, CancellationToken cancellationToken)
        {


            var employeeRepository = unitOfWork.GetRepository<Employee, int>();
            var employee = await employeeRepository.GetById(request.EmployeeId);


            if (employee == null)
                throw new NotFoundException("Employee not found.");


            if (authorizationService.IsSelf(employee.ApplicationUserId))
                throw new ForbiddenException("You cannot change your own activation status.");


            if (employee.IsActive == request.IsActive)
                return "No changes";


            employee.IsActive = request.IsActive;
            employeeRepository.Update(employee);




            if (request.IsActive)
                await userService.UnlockUser(employee.ApplicationUserId); // reactivate login
            else
                await userService.LockUser(employee.ApplicationUserId);   // prevent login

            await unitOfWork.Commit();



            return request.IsActive
    ? "Employee has been activated."
    : "Employee has been deactivated.";


        }
    }
}
