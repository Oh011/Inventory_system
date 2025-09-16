using Application.Exceptions;
using Application.Features.roles.Dtos;
using Application.Features.roles.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Errors;

namespace Infrastructure.Identity.Services
{
    internal class RoleService(RoleManager<IdentityRole> _roleManager) : IRoleService
    {








        public async Task<RoleDto> CreateRoleAsync(string roleName)
        {


            var result = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = roleName,
            });



            if (result.Succeeded)
            {

            }


            var role = await _roleManager.FindByNameAsync(roleName);




            return new RoleDto { RoleName = role.Name, RoleId = role.Id };


        }

        public async Task DeleteRoleAsync(string RoleId)
        {

            var role = await _roleManager.FindByIdAsync(RoleId);


            if (role == null)
            {


                throw new NotFoundException(ErrorMessages.NotFound.Role);
            }



            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .GroupBy(e => e.Code)
                    .ToDictionary(g => g.Key, g => g.Select(e => new ValidationErrorDetail(e.Description)).ToList());

                throw new ValidationException(errors);
            }


        }

        public async Task<List<RoleDto>> GetAllRolesAsync()
        {

            var roles = await _roleManager.Roles.Where(r => r.Name != "Admin").ToListAsync();

            return roles.Select(r => new RoleDto { RoleId = r.Id, RoleName = r.Name }).ToList();

        }
    }
}
