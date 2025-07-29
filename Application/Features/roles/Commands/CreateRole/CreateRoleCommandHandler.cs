using Application.Features.roles.Dtos;
using Application.Features.roles.Interfaces;
using MediatR;

namespace Application.Features.roles.Commands.CreateRole
{
    internal class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
    {

        private readonly IRoleService roleService;

        public CreateRoleCommandHandler(IRoleService roleService)
        {

            this.roleService = roleService;
        }
        public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {

            return await roleService.CreateRoleAsync(request.RoleName);
        }
    }
}
