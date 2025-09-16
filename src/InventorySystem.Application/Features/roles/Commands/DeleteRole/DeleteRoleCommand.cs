using MediatR;

namespace InventorySystem.Application.Features.roles.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest
    {


        public string Id { get; set; }



        public DeleteRoleCommand(string id)
        {

            Id = id;
        }
    }
}
