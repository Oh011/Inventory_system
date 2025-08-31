using MediatR;

namespace Application.Features.Auth.Commands.RevokeRefreshToken
{
    public class LogOutCommand : IRequest<string>
    {
        public string? RefreshToken { get; set; }
        public string DeviceId { get; set; } = default!;
    }
}
