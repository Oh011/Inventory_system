using Application.Exceptions;
using Application.Features.Users.Interfaces;
using InventorySystem.Application.Common.Interfaces.Repositories;
using InventorySystem.Application.Features.Users.Commands.ChangePassword;
using InventorySystem.Application.Features.Users.Dtos;
using Microsoft.AspNetCore.Identity;
using Shared.Errors;
using System.Data;

namespace Infrastructure.Identity.Services
{
    internal class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;


        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
        }

        public async Task<string> AssignRoleAsync(string userId, string Role)
        {



            if (Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                var validationException = new ValidationException(


                 new Dictionary<string, List<ValidationErrorDetail>>()
                 {
                     ["Role"] = new List<ValidationErrorDetail>()
                     {
                    new ValidationErrorDetail("Assigning the 'Admin' role is not allowed.")

                     }
                 }
             );

                throw validationException;

            }

            var user = await _userManager.FindByIdAsync(userId);


            if (user == null)
            {
                throw new NotFoundException("user not found");
            }



            if (!await _roleManager.RoleExistsAsync(Role))
            {



                var validationException = new ValidationException(


                    new Dictionary<string, List<ValidationErrorDetail>>()
                    {
                        ["Role"] = new List<ValidationErrorDetail>()
                {
                    new ValidationErrorDetail($"The role '{Role}' does not exist.")

                }
                    }
                    );




                throw validationException;



            }


            var userRoles = await _userManager.GetRolesAsync(user);



            if (userRoles.Any())
            {

                await _userManager.RemoveFromRolesAsync(user, userRoles);
            }


            var result = await _userManager.AddToRoleAsync(user, Role);


            if (!result.Succeeded)
            {

                var errors = result.Errors.GroupBy(e => e.Code)
                    .ToDictionary(e => e.Key, e => e.Select(e => new ValidationErrorDetail(e.Description)).ToList());


                throw new ValidationException(errors);
            }



            return "Assigned Role Successfully";


        }


        public async Task<string> ChangePassword(ChangePasswordCommand command)
        {

            var user = await _userManager.FindByIdAsync(command.UserId);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }


            var result = await _userManager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .GroupBy(e => e.Code)
                    .ToDictionary(g => g.Key, g => g.Select(e => new ValidationErrorDetail(e.Description)).ToList());

                throw new ValidationException(errors);
            }

            return "Password changed successfully.";




        }

        public async Task<CreateUserResponseDto> CreateUser(CreateUserDto userDto)
        {




            if (!await _roleManager.RoleExistsAsync(userDto.Role))
            {


                var validationException = new ValidationException(


               new Dictionary<string, List<ValidationErrorDetail>>()
               {
                   ["Role"] = new List<ValidationErrorDetail>()
           {
                    new ValidationErrorDetail($"The role '{userDto.Role}' does not exist.")

           }
               }
               );

                throw validationException;

            }





            var userExists = await _userManager.FindByNameAsync(userDto.UserName);


            if (userExists != null)
            {

                throw new UserAlreadyExistsException(userDto.UserName);

            }

            var applicationUser = new ApplicationUser()
            {

                UserName = userDto.UserName,
                Email = userDto.Email,
                FullName = userDto.FullName,

            };


            var result = await _userManager.CreateAsync(applicationUser, userDto.Password);


            if (!result.Succeeded)
            {


                var errors = result.Errors
                           .GroupBy(e => e.Code)
                           .ToDictionary(g => MapToField(g.Key), g => g.Select(e => new ValidationErrorDetail(e.Description)).ToList());



                throw new ValidationException(errors);

            }


            await _userManager.AddToRoleAsync(applicationUser, userDto.Role);




            return new CreateUserResponseDto
            {

                FullName = applicationUser.FullName,
                Email = applicationUser.Email,
                Id = applicationUser.Id,

                Role = userDto.Role,
            };


        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return;


            await _userManager.DeleteAsync(user);
        }

        public Task<List<string>> GetUsersIdByRole(List<string> roles)
        {
            var userIds = _userRepository.GetUsersIdsInRole(roles);


            return userIds;
        }


        public async Task LockUser(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new NotFoundException("User not found");



            user.LockoutEnabled = true;
            user.LockoutEnd = DateTime.MaxValue;

            await _userManager.UpdateAsync(user);



        }

        public async Task UnlockUser(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new NotFoundException("User not found");


            user.LockoutEnd = null;

            await _userManager.UpdateAsync(user);
        }

        private string MapToField(string code)
        {
            code = code.ToLower();

            if (code.Contains("email"))
                return "Email";
            if (code.Contains("password"))
                return "Password";
            if (code.Contains("username"))
                return "Username";

            return "Identity";
        }
    }
}
