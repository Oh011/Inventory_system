using MediatR;

namespace Project.Application.Features.Auth.Commands.ResetPassword
{
    public class RestPasswordCommand : IRequest<string>
    {

        public string Token { get; set; }

        public string Password { get; set; }


        public string Email { get; set; }
    }
}
