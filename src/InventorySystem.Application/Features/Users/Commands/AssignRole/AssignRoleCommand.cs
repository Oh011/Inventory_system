using MediatR;

namespace InventorySystem.Application.Features.Users.Commands.AssignRole
{
    public class AssignRoleCommand : IRequest<string>
    {

        public string UserId { get; set; }


        public string Role { get; set; }

    }
}
