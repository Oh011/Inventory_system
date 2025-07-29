using Application.Features.roles.Dtos;
using Application.Features.roles.Interfaces;
using MediatR;

namespace Application.Features.roles.Queries.GetRoles
{
    internal class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
    {


        private readonly IRoleService roleService;

        public GetRolesQueryHandler(IRoleService roleService)
        {

            this.roleService = roleService;
        }
        public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {


            return await roleService.GetAllRolesAsync();

        }
    }
}
