using MediatR;
using Project.Application.Features.Users.Dtos;

namespace Project.Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordCommand : ChangePasswordDto, IRequest<string>
    {


        public string UserId { get; set; }

    }
}
