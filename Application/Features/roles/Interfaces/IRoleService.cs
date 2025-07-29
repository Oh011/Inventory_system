using Application.Features.roles.Dtos;

namespace Application.Features.roles.Interfaces
{
    public interface IRoleService
    {

        Task<List<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> CreateRoleAsync(string roleName);
        Task DeleteRoleAsync(string roleName);
    }
}
