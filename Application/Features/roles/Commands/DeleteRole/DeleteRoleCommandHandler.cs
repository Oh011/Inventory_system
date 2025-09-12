using Application.Features.roles.Interfaces;
using MediatR;

namespace Project.Application.Features.roles.Commands.DeleteRole
{
    internal class DeleteRoleCommandHandler(IRoleService roleService) : IRequestHandler<DeleteRoleCommand>
    {
        public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {


            await roleService.DeleteRoleAsync(request.Id);
        }
    }
}
