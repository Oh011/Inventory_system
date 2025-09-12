using Application.Features.Auth.Interfaces;
using Application.Features.Auth.Results;
using MediatR;

namespace Application.Features.Auth.Commands.LogIn
{
    internal class LogInUserCommandHandler : IRequestHandler<LogInUserCommand, AuthResultWithRefreshToken>
    {

        private readonly IAuthenticationService authenticationService;


        public LogInUserCommandHandler(IAuthenticationService authenticationService)
        {

            this.authenticationService = authenticationService;
        }
        public async Task<AuthResultWithRefreshToken> Handle(LogInUserCommand request, CancellationToken cancellationToken)
        {


            return await authenticationService.LogIn(request.Email, request.Password, request.DeviceId);
        }
    }
}
