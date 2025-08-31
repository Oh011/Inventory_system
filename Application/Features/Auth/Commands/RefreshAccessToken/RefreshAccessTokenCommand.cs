using Application.Features.Auth.Results;
using MediatR;

namespace Application.Features.Auth.Commands.RefreshAccessToken
{
    public class RefreshAccessTokenCommand : IRequest<AuthResultWithRefreshToken>
    {
        public string? RefreshToken { get; set; }
        public string DeviceId { get; set; } = default!;
    }
}
