using MediatR;

namespace InventorySystem.Application.Features.Employees.Commands.ActivateEmployee
{
    public class ChangeEmployeeActivationStatusCommand : IRequest<string>
    {
        public int EmployeeId { get; set; }
        public bool IsActive { get; set; }
    }
}
