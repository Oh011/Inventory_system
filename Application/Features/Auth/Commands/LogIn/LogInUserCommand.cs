using Application.Features.Auth.Results;
using MediatR;

namespace Application.Features.Auth.Commands.LogIn
{
    public class LogInUserCommand : IRequest<AuthResultWithRefreshToken>
    {

        public string Email { get; set; }


        public string Password { get; set; }

        public string DeviceId { get; set; }  // Add this to track device
    }
}

