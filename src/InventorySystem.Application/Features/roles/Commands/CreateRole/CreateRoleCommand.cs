using Application.Features.roles.Dtos;
using MediatR;

namespace Application.Features.roles.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<RoleDto>
    {


        public string RoleName { get; set; }
    }
}
