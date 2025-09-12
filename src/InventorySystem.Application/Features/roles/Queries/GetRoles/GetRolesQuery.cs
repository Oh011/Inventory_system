using Application.Features.roles.Dtos;
using MediatR;

namespace Application.Features.roles.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<List<RoleDto>>
    {
    }
}
