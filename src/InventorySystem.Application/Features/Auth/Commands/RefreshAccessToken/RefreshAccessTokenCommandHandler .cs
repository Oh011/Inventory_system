using Application.Features.Auth.Interfaces;
using Application.Features.Auth.Results;
using MediatR;

namespace Application.Features.Auth.Commands.RefreshAccessToken
{
    internal class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, AuthResultWithRefreshToken>
    {


        private readonly ITokenService tokenService;

        public async Task<AuthResultWithRefreshToken> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {


            return await tokenService.RefreshAccessTokenAsync(request.RefreshToken, request.DeviceId);
        }
    }
}
