using Project.Application.Features.Users.Commands.ChangePassword;
using Project.Application.Features.Users.Dtos;

namespace Application.Features.Users.Interfaces
{
    public interface IUserService
    {


        Task<CreateUserResponseDto> CreateUser(CreateUserDto userDto);


        Task<List<string>> GetUsersIdByRole(List<string> roles);

        Task UnlockUser(string userId);

        Task LockUser(string userId);

        Task<string> ChangePassword(ChangePasswordCommand command);

        Task DeleteUserAsync(string userId);
        Task<string> AssignRoleAsync(string userId, string Role);
    }
}
