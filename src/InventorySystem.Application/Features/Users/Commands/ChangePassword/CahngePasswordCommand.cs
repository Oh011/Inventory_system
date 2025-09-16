using MediatR;
using InventorySystem.Application.Features.Users.Dtos;

namespace InventorySystem.Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordCommand : ChangePasswordDto, IRequest<string>
    {


        public string UserId { get; set; }

    }
}
