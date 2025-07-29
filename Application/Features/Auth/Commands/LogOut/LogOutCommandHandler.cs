using Application.Features.Auth.Interfaces;
using MediatR;

namespace Application.Features.Auth.Commands.RevokeRefreshToken
{
    internal class LogOutCommandHandler : IRequestHandler<LogOutCommand, string>
    {

        private readonly IAuthenticationService _authenticationService;


        public LogOutCommandHandler(IAuthenticationService authenticationService)
        {

            _authenticationService = authenticationService;
        }
        public async Task<string> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {


            return await _authenticationService.LogOut(request.RefreshToken, request.DeviceId);
        }
    }
}
