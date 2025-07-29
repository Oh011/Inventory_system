using Application.Features.Users.Interfaces;
using MediatR;

namespace Project.Application.Features.Users.Commands.ChangePassword
{
    internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, string>
    {


        private readonly IUserService userService;


        public ChangePasswordCommandHandler(IUserService userService)
        {

            this.userService = userService;
        }
        public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {


            var result = await userService.ChangePassword(request);


            return result;
        }
    }
}
